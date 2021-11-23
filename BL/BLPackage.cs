using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL;

namespace BL
{
    public partial class BL : IBL.IBL
    {

        /// <summary>
        /// The function Add a package in the list of packages in Datasource 
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public int AddPackage(Package package)
        {
            if (!dal.ClientsList().Any(client => client.ID == package.TargetClient.ID)) 
                throw new Exceptions.IdNotFoundException("Receiver Id not found", package.TargetClient.ID);
            if (!dal.ClientsList().Any(client => client.ID == package.SenderClient.ID)) 
                throw new Exceptions.IdNotFoundException("Sender Id not found", package.TargetClient.ID);

            IDAL.DO.Package dalPackage = new IDAL.DO.Package();   //new then assign data then adding to datasource list

            //dalPackage.ID = package.ID;
            dalPackage.SenderId = package.SenderClient.ID;
            dalPackage.TargetId = package.TargetClient.ID;
            dalPackage.Priority = (IDAL.DO.Priorities)package.Priority;
            dalPackage.Weight = (IDAL.DO.WeightCategories)package.Weight;
            dalPackage.PickedUp = DateTime.MinValue;
            dalPackage.Associated = DateTime.MinValue;
            dalPackage.Delivered = DateTime.MinValue;
            dalPackage.Created = DateTime.Now;
            dalPackage.DroneId = 0;
            int id;
            try
            {
                id = dal.AddPackage(dalPackage);
            }
            catch(IDAL.DO.Exceptions.IDException ex )
            {
                throw new Exceptions.IDException("Package ID already exists", ex, dalPackage.ID);  //new bl exception
            }
            return id;
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="droneID"></param>
        public void packageToDrone( int droneID)
        {

            if (!DroneList.Any(d => d.ID == droneID))
                throw new IBL.BO.Exceptions.IdNotFoundException("Drone ID not found", droneID);

            DroneToList drone = DroneList.First(x => x.ID == droneID);
            if(!(drone.Status == DroneStatus.Available))         //if Drone not available
            {
                throw new Exceptions.UnableAssociatPackage("Drone is not Available");// new except
            }

            List<IDAL.DO.Package> dalPackages = new List<IDAL.DO.Package>(dal.PackageList().ToList().FindAll(x => (int)(x.Weight) <= (int) drone.MaxWeight  &&  x.Associated == DateTime.MinValue)); // כל החבילות שיתכן שהרחפן יכול לקחת
            IDAL.DO.Package package = new IDAL.DO.Package() ;
            int priority = 2, weight = (int)drone.MaxWeight;
            bool flag = true;

            while (flag)
            {
                List<IDAL.DO.Package> filteredPackages = dalPackages.FindAll(p=> p.Priority == (IDAL.DO.Priorities)priority); // רשימת החבילות בעדיפות הנוכחית
                if(filteredPackages.Count() == 0)
                {
                    if (priority != 0)
                    {
                        priority--; // אם אין חבילות בעדיפות הנוכחית נרד עדיפות
                        weight = (int)drone.MaxWeight; // אם ירדנו עדיפות נאפס את המשקל כי הסינון מתחיל מחדש
                        continue; // נתחיל מחדש את הלולאה
                    }
                    else throw new IBL.BO.Exceptions.UnableAssociatPackage("There is no package for the the Drone"); // אם זה הגיע ל0 לא נשארו חבילות
                }

                filteredPackages = filteredPackages.FindAll(p => p.Weight == (IDAL.DO.WeightCategories)weight); // מתוך רשימת החבילות בעדיפות נוכחית נסנן את המשקל הגבוה שקיים
                if(filteredPackages.Count() == 0)
                {
                    if(weight != 0)
                    {
                        weight--; // אם לא מצאנו במשקל הנוכחי נרד משקל
                        continue;
                    }
                    else
                    {
                        if (priority != 0)priority--; // אם לא מצאנו בעדיפות הנוכחית חבילה שמתאימה למשקל נרד עדיפות
                        weight = (int)drone.MaxWeight; // ואם ירדנו עדיפות נאפס את המשקל
                        continue;
                    }
                }

                package = NearestPackageToDrone(droneID, filteredPackages);                //send to function that return from the list the nearest package from drone
                IDAL.DO.Client senderClient = dal.ClientById(package.SenderId);                                 //sender client, his location is the package location
                IDAL.DO.Client targetClient = dal.ClientById(package.TargetId);                                //target client target location for the drone

                double minBattery = batteryConsumption(drone.DroneLocation.Latitude, drone.DroneLocation.Longitude, senderClient.Latitude, senderClient.Longitude, 3);        // from drone location to package with no weight
                minBattery += batteryConsumption(senderClient.Latitude, senderClient.Longitude, targetClient.Latitude, targetClient.Longitude, (int)package.Weight);         // from package sender location to target with package weight

                IDAL.DO.Station station = NearestStationToClient(package.TargetId);                                                              //station with available chargeSlot nearest to client target for charging the drone if needed
                minBattery += batteryConsumption(targetClient.Latitude, targetClient.Longitude, station.Latitude, station.Longitude, 3);         //from client target to nearest station location with no weight

                if (minBattery > drone.Battery) dalPackages.Remove(package); // אם החבילה לא מתאימה נמחק אותה מרשימת החבילות האופציונליות
                else flag = false;
            }

            //update data if fitting package found
            int index = DroneList.FindIndex(d => d.ID == drone.ID);
            DroneList[index].Status = DroneStatus.Shipping; //Update of drone status, logical 
            dal.packageToDrone(package, droneID); // update of drone, datasource




            //List<IDAL.DO.Package> dalPackageHighPriority = new List<IDAL.DO.Package>();

            //for (int i = 2; i >= 0; i--)                 //creating  new package list that havent been associated with highest priority
            //{
            //    dalPackageHighPriority = dal.PackageList().ToList().FindAll(x => (int)(x.Priority) == i && x.Associated == DateTime.MinValue);  //first getting priority equal 2 (urgent) ect
            //    if (dalPackageHighPriority.Count() > 0) break;
            //}
            //if (dalPackageHighPriority.Count() == 0)                 //if no element got in the list which means no package matches
            //    throw new IBL.BO.Exceptions.UnableAssociatPackage("There are no packages waiting to be shipped");


            //List<IDAL.DO.Package> PackagesSuitableWeight = dalPackageHighPriority.ToList().FindAll(x => ((int)(x.Weight) <= (int)(drone.MaxWeight)));          // from the list created we now search for packages that matches drone max weight capacity
            //if (PackagesSuitableWeight.Count() == 0)
            //    throw new IBL.BO.Exceptions.UnableAssociatPackage("There are no packages suitable for the weight of the Drone");

            //IDAL.DO.Package package = NearestPackageToDrone(droneID, PackagesSuitableWeight);                //send to function that return from the list the nearest package from drone
            //IDAL.DO.Client senderClient = dal.ClientById(package.SenderId);                                 //sender client, his location is the package location
            //IDAL.DO.Client targetClient = dal.ClientById(package.TargetId);                                //target client target location for the drone

            //double minBattery = batteryConsumption(drone.DroneLocation.Latitude, drone.DroneLocation.Longitude, senderClient.Latitude, senderClient.Longitude, 3);        // from drone location to package with no weight
            //minBattery += batteryConsumption(senderClient.Latitude, senderClient.Longitude,targetClient.Latitude ,targetClient.Longitude , (int)package.Weight);         // from package sender location to target with package weight

            //IDAL.DO.Station station = NearestStationToClient(package.TargetId);                                                              //station with available chargeSlot nearest to client target for charging the drone if needed
            //minBattery += batteryConsumption(targetClient.Latitude, targetClient.Longitude, station.Latitude,station.Longitude,3 );         //from client target to nearest station location with no weight

            //if (minBattery > drone.Battery) throw new IBL.BO.Exceptions.UnableAssociatPackage("Not enough battery");                     // all of above in condition that the battery of the drone is enough to travel

            ////update data if fitting package found
            //int index = DroneList.FindIndex(d => d.ID == drone.ID);
            //DroneList[index].Status = DroneStatus.Shipping; //Update of drone status, logical 
            //dal.packageToDrone(package, droneID); // update of drone, datasource

        }

        /// <summary>
        /// the function get a drone to pick up a package
        /// </summary>
        /// <param name="droneID"></param>
        public void PickedUpByDrone(int droneID)
        {
            if (! DroneList.Any(d => d.ID == droneID)) throw new IBL.BO.Exceptions.IdNotFoundException("Drone ID not found", droneID);
            DroneToList drone = DroneList.First(x => x.ID == droneID);
            if ((drone.Status != DroneStatus.Shipping)) throw new Exceptions.UnablePickedUpPackage("Drone is not Shipping", droneID); // רחפן לא מבצע משלוח
            if (!dal.PackageList().Any(p => p.DroneId == droneID)) throw new IBL.BO.Exceptions.UnablePickedUpPackage("No package associated with the drone was found"); // אין חבילה ששויכה לרחפן הזה

            IDAL.DO.Package package = dal.PackageList().First(p => p.DroneId == droneID && p.PickedUp == DateTime.MinValue);
            if (package.PickedUp != DateTime.MinValue) throw new IBL.BO.Exceptions.UnablePickedUpPackage("The package has already been PickedUp", package.ID); // אם החבילה שמשוייכת לרחפן כבר נאספה אז תזרוק חריגה

            IDAL.DO.Client sender = dal.ClientById(package.SenderId); // השולח של החבילה - מיקום החדש של הרחפן 
            int index = DroneList.FindIndex(d => d.ID == droneID);
             
            // עדכון בשכבת הלוגיקה
            double spendBattery = batteryConsumption(drone.DroneLocation.Latitude, drone.DroneLocation.Longitude, sender.Latitude, sender.Longitude, 3); // ממיקום הרחפן לחבילה במשקל ריק
            if ((DroneList[index].Battery - spendBattery) < 0) throw new IBL.BO.Exceptions.UnablePickedUpPackage("Not enough battery");
            DroneList[index].Battery -= (int)Math.Round(spendBattery);
            DroneList[index].DroneLocation.Latitude = sender.Latitude;
            DroneList[index].DroneLocation.Longitude = sender.Longitude;

            //עדכון בשכבת הנתונים
            dal.PickedUpByDrone(package);

        }


        public void DeliveredToClient(int droneID)
        {
            if (!DroneList.Any(d => d.ID == droneID)) throw new IBL.BO.Exceptions.IdNotFoundException("Drone ID not found", droneID);
            DroneToList drone = DroneList.First(x => x.ID == droneID);
            if ((drone.Status != DroneStatus.Shipping)) throw new Exceptions.UnablePickedUpPackage("Drone is not Shipping", droneID); // רחפן לא מבצע משלוח
            if (!dal.PackageList().Any(p => p.DroneId == droneID)) throw new IBL.BO.Exceptions.UnablePickedUpPackage("No package associated with the drone was found"); // אין חבילה ששויכה לרחפן הזה

            IDAL.DO.Package package = dal.PackageList().First(p => p.DroneId == droneID && p.Delivered == DateTime.MinValue);
            if (package.Delivered > DateTime.MinValue) throw new IBL.BO.Exceptions.UnablePickedUpPackage("The package has already been PickedUp", package.ID); // אם החבילה שמשוייכת לרחפן כבר סופקה אז תזרוק חריגה

            IDAL.DO.Client target = dal.ClientById(package.TargetId);// היעד של החבילה - מיקום הרחפן החדש
            int index = DroneList.FindIndex(d => d.ID == droneID);

            // עדכון בשכבת הלוגיקה
            double spendBattery = batteryConsumption(drone.DroneLocation.Latitude, drone.DroneLocation.Longitude, target.Latitude, target.Longitude, (int)package.Weight); // ממיקום הרפן (מיקום השולח) למיקום היעד במשקל החבילה
            if ((DroneList[index].Battery - spendBattery) < 0) throw new IBL.BO.Exceptions.UnablePickedUpPackage("Not enough battery");
            DroneList[index].Battery -= (int)Math.Round(spendBattery);
            DroneList[index].DroneLocation.Latitude = target.Latitude;
            DroneList[index].DroneLocation.Longitude = target.Longitude;
            DroneList[index].Status = DroneStatus.Available;

            //עדכון בשכבת הנתונים
            dal.DeliveredToClient(package);

        }

        /// <summary>
        /// the function find and display package information according to user id input
        /// </summary>
        /// <param name="packageID"></param>
        /// <returns></returns>
        public Package DisplayPackage(int packageID)
        {
            IDAL.DO.Package dalPackage;
            try
            {
                dalPackage = dal.PackageById(packageID);    //if package not found 
            }
            catch (IDAL.DO.Exceptions.IDException ex)
            {
                throw new IBL.BO.Exceptions.IdNotFoundException("Unable to view package, ID not found", ex);
            }

            // assigning 
            Package blPackage = new Package();

            blPackage.ID = dalPackage.ID; 
            blPackage.Weight = (IBL.BO.WeightCategories)(dalPackage.Weight);
            blPackage.Priority = (IBL.BO.Priorities)(dalPackage.Priority);

            blPackage.Created = dalPackage.Created;
            blPackage.Associated = dalPackage.Associated;
            blPackage.PickedUp = dalPackage.PickedUp;
            blPackage.Delivered = dalPackage.Delivered;

            //init in package attributes of sender and target
            IDAL.DO.Client sender, target;
            try
            {
                sender = dal.ClientById(dalPackage.SenderId); // client that sent the package
            }
            catch (IDAL.DO.Exceptions.IDException ex)
            {
                throw new IBL.BO.Exceptions.IdNotFoundException("Unable to view package, senderID not found",ex);
            }
            try
            {
                target = dal.ClientById(dalPackage.TargetId); //client that gets the package 
            }
            catch (IDAL.DO.Exceptions.IDException ex)
            {
                throw new IBL.BO.Exceptions.IdNotFoundException("Unable to view package, targetID not found", ex);
            }
            blPackage.SenderClient = new ClientPackage();
            blPackage.TargetClient = new ClientPackage();

            blPackage.SenderClient.ID = sender.ID;
            blPackage.SenderClient.Name = sender.Name;
            blPackage.TargetClient.ID = target.ID;
            blPackage.TargetClient.Name = target.Name;

            if(dalPackage.Associated == DateTime.MinValue) //if package is not associated we can return it
            {
                blPackage.DroneOfPackage = null;
                return blPackage;
            }

            //assigning drone that takes the package id associated
            DroneOfPackage droneOfPackage = new DroneOfPackage(); //object of droneOfPackage then assigning it
            droneOfPackage.CurrentLocation = new Location();
            if (!DroneList.Any(d => d.ID == dalPackage.DroneId))
                throw new IBL.BO.Exceptions.IdNotFoundException("Unable to view package, The package was associated but no drone ID was found", dalPackage.DroneId);
            DroneToList drone = DroneList.Find(d => d.ID == dalPackage.DroneId);

            droneOfPackage.Id = drone.ID;
            droneOfPackage.Battery = drone.Battery;
            droneOfPackage.CurrentLocation = drone.DroneLocation;

            blPackage.DroneOfPackage = droneOfPackage; //assign drone to the package 

            return blPackage;
        }

        /// <summary>
        /// The function Display list of all packages information
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PackageToList> DisplayPackageList()
        {
            List<PackageToList> packages = new List<PackageToList>(); 

            foreach (var dalPackage in dal.PackageList())                  //going through every package from package list and adding it to the list created
            {
                PackageToList packageToList = new PackageToList();        //create new package to the list, assing its values

                packageToList.Id = dalPackage.ID;
                packageToList.Sender = dal.ClientById(dalPackage.SenderId).Name;
                packageToList.Receiver = dal.ClientById(dalPackage.TargetId).Name;
                packageToList.Weight = (WeightCategories)dalPackage.Weight;
                packageToList.Priority = (Priorities)dalPackage.Priority;

                if (dalPackage.Associated == DateTime.MinValue) packageToList.Status = PackageStatus.Created;
                else if (dalPackage.PickedUp == DateTime.MinValue) packageToList.Status = PackageStatus.Associated;
                else if (dalPackage.Delivered == DateTime.MinValue) packageToList.Status = PackageStatus.PickedUp;
                else packageToList.Status = PackageStatus.Delivered;

                packages.Add(packageToList);        //adding it to the package list
            }

            return packages;                 //returns the list of packages
        }

        /// <summary>
        /// function that return list of package that havent been associated
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PackageToList> DisplayPackageListWithoutDrone()
        {
            IEnumerable<PackageToList> packagesWithoutDrone = DisplayPackageList();     //getting new list from the bl package list fonctions

            packagesWithoutDrone = packagesWithoutDrone.Where(p => p.Status == PackageStatus.Created); // filter packages list and getting the ones that havent been associated which is only created
            return packagesWithoutDrone;
        }

        /// <summary>
        /// The fonction finds the nearest package to drone from fitting package list that responded correctly from the conditions of highest priority and suitable weight 
        /// </summary>
        /// <param name="DroneID"></param>
        /// <param name="PackagesSuitableWeight"></param>
        /// <returns></returns>
        private IDAL.DO.Package NearestPackageToDrone(int DroneID, List<IDAL.DO.Package> PackagesSuitableWeight)  
        {
            DroneToList drone = DroneList.Find(x => x.ID == DroneID);       //finding the drone
            IDAL.DO.Package tempPackage = new IDAL.DO.Package();

            double distance = int.MaxValue;

            foreach (var package in PackagesSuitableWeight)             //finding min distance between drone and package
            {
                IDAL.DO.Client sender = dal.ClientById(package.SenderId);        //sender from data source that want to send package
                double tempDistance = DalObject.DalObject.distance(drone.DroneLocation.Latitude, drone.DroneLocation.Longitude, sender.Latitude, sender.Longitude);  //calculate distance
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    tempPackage = package;
                }
            }
            return tempPackage;
        }

    }
}
