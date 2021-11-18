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


        public Package GetPackage(int id)
        {
            Package package = default;
            try
            {
                IDAL.DO.Package dalPackage = dal.PackageById(id);

            }
            catch (IDAL.DO.Exceptions.IDException pex)
            {
                throw new IBL.BO.Exceptions.BLPackageException($"Package ID {id} not found",pex);
            }

            return package;
        }


        public void AddPackage(Package package)
        {

            try
            {
                if (package.ID < 0) throw new IBL.BO.Exceptions.PackageIdException("Package Id cannot be negative", package.ID);
            }
            catch(IBL.BO.Exceptions.PackageIdException ex)
            {
                //if (ex.Message == "Package Id cannot be negative") { throw; }
            }

            if (!dal.ClientsList().Any(client => client.ID == package.ReceiverClient.ID)) throw new Exceptions.IdNotFoundException("Receiver Id not found", package.ReceiverClient.ID);
            if (!dal.ClientsList().Any(client => client.ID == package.SenderClient.ID)) throw new Exceptions.IdNotFoundException("Sender Id not found", package.ReceiverClient.ID);

            IDAL.DO.Package dalPackage = new IDAL.DO.Package();

            dalPackage.ID = package.ID;
            dalPackage.SenderId = package.SenderClient.ID;
            dalPackage.TargetId = package.ReceiverClient.ID;
            dalPackage.Priority = (IDAL.DO.Priorities)package.Priority;
            dalPackage.Weight = (IDAL.DO.WeightCategories)package.Weight;
            dalPackage.PickedUp = DateTime.MinValue;
            dalPackage.Associated = DateTime.MinValue;
            dalPackage.Delivered = DateTime.MinValue;
            dalPackage.Created = DateTime.Now;
            dalPackage.DroneId = 0;
            try
            {
                dal.AddPackage(dalPackage);
            }
            catch(IDAL.DO.Exceptions.IDException ex )
            {
                throw new Exceptions.IDException("Package ID already exists", ex, dalPackage.ID);
            }

        }

       

        public void packageToDrone( int droneID)
        {

            if (!DroneList.Any(d => d.ID == droneID)) throw new IBL.BO.Exceptions.IdNotFoundException("Drone ID not found", droneID);
            DroneToList drone = DroneList.First(x => x.ID == droneID);
            if(!(drone.Status == DroneStatus.Available)) // רחפן לא זמין
            {
                throw new Exceptions.DroneTaken("Drone is not Available", droneID);// new except
            }
            


            List<IDAL.DO.Package> dalPackageHighPriority = new List<IDAL.DO.Package>();
            for (int i = 2; i >= 0; i--) //      יתן לי רשימה של החבילות שלא שויכו בעדיפות הגבוהה ביותר הקיימת
            {
                dalPackageHighPriority = dal.PackageList().ToList().FindAll(x => (int)(x.Priority) == i && x.Associated == DateTime.MinValue);
                if (dalPackageHighPriority.Count() > 0) break;
            }
            if (dalPackageHighPriority.Count() == 0) throw new IBL.BO.Exceptions.NotFound("There are no packages waiting to be shipped");


            List<IDAL.DO.Package> PackagesSuitableWeight = dalPackageHighPriority.ToList().FindAll(x => ((int)(x.Weight) <= (int)(drone.MaxWeight))); //   מתוך הרשימה של העדיפויות זה יחזיק לי רשימה של חבילות שהרחפן יכול לקחת

            IDAL.DO.Package package = NearestPackageToDrone(droneID, PackagesSuitableWeight); // שליחה לפונקציה שתחזיר לי מתוך הרשימה הזאת את החבילה הקרובה לרחפן
            IDAL.DO.Client senderClient = dal.ClientById(package.SenderId); // זה הלקוח ששולח את החבילה - מיקומו זה מיקום החבילה
            IDAL.DO.Client targetClient = dal.ClientById(package.TargetId); // זה הלקוח שמקבל את החבילה - זה מיקום הרחפן בסיום המשלוח

            double minBattery = batteryConsumption(drone.DroneLocation.Latitude, drone.DroneLocation.Longitude, senderClient.Latitude, senderClient.Longitude, 3); // ממיקום הרחפן לחבילה במשקל ריק
            minBattery += batteryConsumption(senderClient.Latitude, senderClient.Longitude,targetClient.Latitude ,targetClient.Longitude , (int)package.Weight); // ממיקום החבילה ליעד במשקל החבילה

            IDAL.DO.Station station = NearestStationToClient(package.TargetId); // התחנה עם עמדות טעינה פנוות הקרובה ליעד המשלוח
            minBattery += batteryConsumption(targetClient.Latitude, targetClient.Longitude, station.Latitude,station.Longitude,3 ); // מיעד החבילה לתחנה הקרובה במשקל ריק


            // עדכון הנתונים אם חבילה נמצאה
            int index = DroneList.FindIndex(d => d.ID == drone.ID);
            DroneList[index].Status = DroneStatus.Shipping;

            IDAL.DO.Package packageTemp = package;
            packageTemp.DroneId = drone.ID;

        }


        void PickedUpByDrone(int droneID)
        {
            if (! DroneList.Any(d => d.ID == droneID)) throw new IBL.BO.Exceptions.IdNotFoundException("Drone ID not found", droneID);
            DroneToList drone = DroneList.First(x => x.ID == droneID);
            if ((drone.Status != DroneStatus.Shipping)) throw new Exceptions.DroneTaken("Drone is not Shipping", droneID); // רחפן לא מבצע משלוח
            if (!dal.PackageList().Any(p => p.DroneId == droneID)) throw new IBL.BO.Exceptions.IdNotFoundException("No package associated with the drone was found",droneID);

            IDAL.DO.Package package = dal.PackageList().First(p => p.DroneId == droneID);
            


        }


        private IDAL.DO.Package NearestPackageToDrone(int DroneID, List<IDAL.DO.Package> PackagesSuitableWeight) // חישוב החבילה הקרובה לרחפן מתוך רשימת החבילות שבדחיפות הגבוהה ושמתאימים למשקל הרחפן 
        {
            DroneToList drone = DroneList.Find(x => x.ID == DroneID);
            IDAL.DO.Package tempPackage = new IDAL.DO.Package();
            double distance = int.MaxValue;

            foreach (var package in PackagesSuitableWeight)
            {
                IDAL.DO.Client sender = dal.ClientById(package.SenderId);// הלקוח שהחבילה שלו - (מיקום השולח זה מיקום החבילה
                double tempDistance = DalObject.DalObject.distance(drone.DroneLocation.Latitude, drone.DroneLocation.Longitude, sender.Latitude, sender.Longitude);
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
