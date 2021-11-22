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
        static Random rand = new Random();
        public List<DroneToList> DroneList = new List<DroneToList>();
        public IDAL.IDAL dal;
        public double PowerVacantDrone;
        public double PowerLightDrone;
        public double PowerMediumDrone;
        public double PowerHeavyDrone;
        public double ChargeRate;
        //private int droneID;

        public BL()
        {
            dal = new DalObject.DalObject();

            PowerVacantDrone = (dal.PowerConsumptionByDrone())[0];
            PowerLightDrone = (dal.PowerConsumptionByDrone())[1];
            PowerMediumDrone = (dal.PowerConsumptionByDrone())[2];
            PowerHeavyDrone = (dal.PowerConsumptionByDrone())[3];
            ChargeRate = (dal.PowerConsumptionByDrone())[4];

            initializeDrone();

        }


        private void initializeDrone()
        {

            foreach (var drone in dal.DroneList())
            {
                DroneToList droneToList = new DroneToList();
                droneToList.DroneLocation = new Location();
                droneToList.ID = drone.ID;
                droneToList.Model = drone.Model;
                droneToList.MaxWeight = (WeightCategories)drone.MaxWeight;

                bool flag = false;
                foreach (var package in dal.PackageList())
                {

                    if ((package.DroneId == drone.ID) && (package.Delivered == DateTime.MinValue)) // אם שוייך אך לא נמסר
                    {
                        flag = true;
                        droneToList.Status = DroneStatus.Shipping;
                        droneToList.PackageID = package.ID;

                        if (package.PickedUp == DateTime.MinValue) // מיקום הרחפן אם עדיין לא נאסף
                        {
                            IDAL.DO.Station location = NearestStationToClient(package.SenderId);
                            droneToList.DroneLocation.Latitude = location.Latitude;
                            droneToList.DroneLocation.Longitude = location.Longitude;
                        }
                        else // מיקום הרחפן אם נאסף 
                        {
                            droneToList.DroneLocation.Latitude = dal.ClientById(package.SenderId).Latitude;
                            droneToList.DroneLocation.Longitude = dal.ClientById(package.SenderId).Longitude;
                        }

                        // מצב סוללת הרחפן אם שוייך אך לא נמסר
                        Location targetLocation = new Location();
                        targetLocation.Latitude = dal.ClientById(dal.PackageById(droneToList.PackageID).TargetId).Latitude;
                        targetLocation.Longitude = dal.ClientById(dal.PackageById(droneToList.PackageID).TargetId).Longitude;

                        double minBattery;
                        minBattery = batteryConsumption(droneToList.DroneLocation.Latitude, droneToList.DroneLocation.Longitude, targetLocation.Latitude, targetLocation.Longitude, (int)package.Weight);
                        //minBattery = BatteryByKM((int)package.Weight, KM);

                        IDAL.DO.Station stationLocation = NearestStationToClient(package.TargetId);
                        minBattery += batteryConsumption(targetLocation.Latitude, targetLocation.Longitude, stationLocation.Latitude, stationLocation.Longitude,3);
                        //minBattery += BatteryByKM(3, KM);

                        if (minBattery > 100) throw new Exception();
                        droneToList.Battery = rand.Next((int)minBattery+1, 101); // בין הצריכה המינמלית ל100

                        break;
                    }
                }

                if (!flag)
                {
                    droneToList.Status = (DroneStatus)(rand.Next(0, 2));
                    if (droneToList.Status == DroneStatus.Maintenance)
                    {
                        IDAL.DO.Station station = dal.StationWithCharging().ElementAt(rand.Next(0, dal.StationsList().Count()));
                        droneToList.DroneLocation.Latitude = station.Latitude;
                        droneToList.DroneLocation.Longitude = station.Longitude;
                        droneToList.Battery = rand.Next(0, 21);

                        dal.DroneCharge(drone, station.ID); // הוספה לטעינה
                    }
                    else
                    {
                        int index = rand.Next(0, 10);
                        while (dal.PackageList().ElementAt(index).Delivered == DateTime.MinValue)
                        {
                            index = rand.Next(0, 10);
                        }
                        int clientID = dal.PackageList().ElementAt(index).TargetId;

                        droneToList.DroneLocation.Latitude = dal.ClientById(clientID).Latitude;
                        droneToList.DroneLocation.Longitude = dal.ClientById(clientID).Longitude;

                        double minBattery;
                        IDAL.DO.Station stationLocation = NearestStationToClient(dal.ClientById(clientID).ID);
                        minBattery = batteryConsumption(droneToList.DroneLocation.Latitude, droneToList.DroneLocation.Longitude, stationLocation.Latitude, stationLocation.Longitude,3);
                        //minBattery = BatteryByKM(3, KM);
                        droneToList.Battery = rand.Next((int)minBattery+1, 101);

                    }
                }
                DroneList.Add(droneToList);
            }
        }


        public void AddDrone(Drone drone, int stationNumToCharge)
        {

            if (drone.ID < 0) throw new IBL.BO.Exceptions.IDException("Drone ID can not be negative", drone.ID);
            if (!dal.StationsList().Any(x => x.ID == stationNumToCharge)) throw new IBL.BO.Exceptions.IDException("Station ID not found", stationNumToCharge);
            if (dal.StationById(stationNumToCharge).ChargeSlots <= 0) throw new IBL.BO.Exceptions.SendingDroneToCharging("There are no charging slots available at the station", stationNumToCharge);


            IDAL.DO.Drone droneDAL = new IDAL.DO.Drone(); // הוספה לרשימה ב DAL
            droneDAL.ID = drone.ID;
            droneDAL.Model = drone.Model;
            droneDAL.MaxWeight = (IDAL.DO.WeightCategories)(int)(drone.MaxWeight);
            try // חריגה משכבת הנתונם
            {
                dal.AddDrone(droneDAL);
            }
            catch (IDAL.DO.Exceptions.IDException ex)
            {
                throw new Exceptions.IDException("A Drone ID already exists", ex, droneDAL.ID);
            }
          

            DroneToList droneToList = new DroneToList();
            droneToList.DroneLocation = new Location();
            droneToList.ID = drone.ID;
            droneToList.Model = drone.Model;
            droneToList.MaxWeight = drone.MaxWeight;
            droneToList.Battery = rand.Next(20, 41);
            droneToList.Status = DroneStatus.Maintenance;
            droneToList.DroneLocation.Latitude = dal.StationById(stationNumToCharge).Latitude;
            droneToList.DroneLocation.Longitude = dal.StationById(stationNumToCharge).Longitude;
            droneToList.PackageID = 0;
            DroneList.Add(droneToList);
         }



        public void UpdateDroneName(int id, string name)
        {
            IDAL.DO.Drone droneDAL;
            try
            {
                if (!DroneList.Any(x => x.ID == id)) throw new IBL.BO.Exceptions.IDException("Drone ID not found", id); // // חריגה משהכבה הלוגית
                droneDAL = dal.DroneById(id); // חריגה משכבת הנתונים
            }
            catch (IBL.BO.Exceptions.IDException ex) { throw; }
            catch (IDAL.DO.Exceptions.IDException ex) { throw; }

            foreach (var droneBL in DroneList) // עדכון ברשימה ב BL
            {
                if (droneBL.ID == id)
                {
                    droneBL.Model = name;
                    break;
                }
            }
            IDAL.DO.Drone droneDalTemp = droneDAL; // עדכון ברשימה בשכבת התונים
            droneDalTemp.Model = name;
            try // לא אמור להיות חריגה כי כבר בדקנו בתחילת הפונקציה שזה קיים
            {
                dal.DeleteDrone(droneDAL);
                dal.AddDrone(droneDalTemp);
            }
            catch (IDAL.DO.Exceptions.IDException ex) { throw new IBL.BO.Exceptions.IDException("Fault in drone update. Was not supposed to be an exception because we have already checked before", ex, id); }
        }




        public void ChargeDrone(int id)
        {
            DroneToList drone = DroneList.Find(drone => drone.ID == id);
            if (drone == null) { throw new IBL.BO.Exceptions.IDException("Drone ID not found", id); }

            if (drone.Status == DroneStatus.Available)
            {
                IDAL.DO.Station station = NearestStationToDrone(drone.ID);
                double minBattery = batteryConsumption(drone.DroneLocation.Latitude, drone.DroneLocation.Longitude, station.Latitude, station.Longitude, 4); // כמות הסוללה הנדרשת כדי שהרחפן יוכל לטוס ממיקומו ועד התחנה

                if (drone.Battery < minBattery) throw new IBL.BO.Exceptions.SendingDroneToCharging("The drone can not reach the station, Not enough battery",drone.ID);

                // ניתן לשלוח לטעינה
                DroneToList tempDrone = drone;

                tempDrone.Battery -= (int) Math.Round(minBattery); // הרחפן כביכול נסע לתחנה ולכן צריך להוריד ממנו את הסוללה של הנסיעה מהמיקום שלו לתחנה
                tempDrone.DroneLocation.Latitude = station.Latitude;
                tempDrone.DroneLocation.Longitude = station.Longitude;
                tempDrone.Status = DroneStatus.Maintenance;

                dal.DroneCharge(dal.DroneById(drone.ID), station.ID); // יצירת מופע של רחפן בטעינה והקטנת מספר עמדות הטעינה

            }
            else { throw new IBL.BO.Exceptions.SendingDroneToCharging("Drone status is not Available", drone.ID); }

        }

        public void FinishCharging(int droneID, double minutesCharging)   
        {
            
            if (!DroneList.Any(drone => drone.ID == droneID)) throw new IBL.BO.Exceptions.IDException("Drone ID not found", droneID);
            if (!dal.droneChargesList().Any(d => d.DroneId == droneID)) throw new IBL.BO.Exceptions.EndDroneCharging("The status of the drone is charging but it is not in the droneCharges list", droneID);
            int indexDroneToList = DroneList.FindIndex(d => d.ID == droneID);
            if (DroneList.Find(drone => drone.ID == droneID).Status != DroneStatus.Maintenance) throw new IBL.BO.Exceptions.EndDroneCharging("Drone status is not Maintenance", droneID);

            //int minutesCharging =(int) ((DateTime.Now - dal.droneChargesList().First(x => x.DroneId == droneID).ChargingStartTime).TotalMinutes); // המרה מspentime

            int battary =(int)((minutesCharging / 60.0) * 100);
            if (battary > 100) battary = 100;
            DroneList[indexDroneToList].Battery = battary;
            DroneList[indexDroneToList].Status = DroneStatus.Available;

            IDAL.DO.DroneCharge droneCharge = dal.droneChargesList().First(d => d.DroneId == droneID);

            dal.FinishCharging(droneCharge); // שליחה לפונקציה שתקטין עמדות טעינה ותמחוק מופע של רחפן בטעינה

        }

        public Drone DisplayDrone(int id)
        {
            if (!DroneList.Any(d => d.ID == id))
                throw new IBL.BO.Exceptions.IDException("Drone ID not found", id);
            DroneToList droneToList = DroneList.Find(d => d.ID == id);

            Drone drone = new Drone();
            drone.DronePackageProcess = new PackageProcess();

            drone.ID = droneToList.ID;
            drone.Battery = droneToList.Battery;
            drone.DroneLocation = droneToList.DroneLocation;
            drone.MaxWeight = droneToList.MaxWeight;
            drone.Model = droneToList.Model;
            drone.Status = droneToList.Status;

            if (drone.Status == DroneStatus.Shipping) //  אם הוא מעביר חבילה מאתחלים את מופע משלוח החבילה
            {
                IDAL.DO.Package package = dal.PackageList().First(x => x.DroneId == drone.ID);
                drone.DronePackageProcess.Id = package.ID;
                drone.DronePackageProcess.Priority = (Priorities)(package.Priority);
                drone.DronePackageProcess.Weight = (WeightCategories)(package.Weight);
                if (package.PickedUp == DateTime.MinValue) drone.DronePackageProcess.PackageShipmentStatus = ShipmentStatus.Waiting;
                else drone.DronePackageProcess.PackageShipmentStatus = ShipmentStatus.OnGoing;

                ClientPackage sender = new ClientPackage();
                ClientPackage receiver = new ClientPackage();
                Location collectLocation = new Location();
                Location destinationLocation = new Location();

                sender.ID = package.SenderId; // איתחולי לקוח בחבילה
                sender.Name = dal.ClientById(sender.ID).Name;
                receiver.ID = package.TargetId;
                receiver.Name = dal.ClientById(receiver.ID).Name;
                drone.DronePackageProcess.Sender = sender;
                drone.DronePackageProcess.Receiver = receiver;

                collectLocation.Latitude = dal.ClientById(sender.ID).Latitude; // איתחולי מיקומים של המקור ויעד
                collectLocation.Longitude = dal.ClientById(sender.ID).Longitude;
                destinationLocation.Latitude = dal.ClientById(receiver.ID).Latitude;
                destinationLocation.Longitude = dal.ClientById(receiver.ID).Longitude;
                drone.DronePackageProcess.CollectLocation = collectLocation;
                drone.DronePackageProcess.DestinationLocation = destinationLocation;

                drone.DronePackageProcess.Distance = DalObject.DalObject.distance(drone.DronePackageProcess.CollectLocation.Latitude, drone.DronePackageProcess.CollectLocation.Longitude,
                drone.DronePackageProcess.DestinationLocation.Latitude, drone.DronePackageProcess.DestinationLocation.Longitude);
            }
            else drone.DronePackageProcess = null;

            return drone;
        }


        public IEnumerable<DroneToList> DisplayDroneList()
        {
            List<DroneToList> drones = new List<DroneToList>(DroneList); // העתקה של רשימה ללא רפרנס!!!
            return drones;
        }

        private IDAL.DO.Station NearestStationToDrone(int DroneID) // חישוב התחנה הקרובה לרחפן עם עמדות פנויות והחזרה שלה
        {
            DroneToList drone = DroneList.Find(x => x.ID == DroneID);
            IDAL.DO.Station tempLocation = new IDAL.DO.Station();
            double distance = int.MaxValue;

            if (dal.StationWithCharging().Count() == 0) throw new IBL.BO.Exceptions.SendingDroneToCharging("There are no charging slots available at any station",DroneID); // אם אין עמדות טעינה פנויות באף תחנה
            foreach (var station in dal.StationWithCharging())
            {

                double tempDistance = DalObject.DalObject.distance(drone.DroneLocation.Latitude, drone.DroneLocation.Longitude, station.Latitude, station.Longitude);
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    tempLocation = station;
                }

            }
            return tempLocation;
        }


        private double batteryConsumption(double lat1, double long1, double lat2, double long2, int weight) // חישוב צריכת סוללה בין 2 נקודות ולפי משקל החבילה
        {
            double KM = DalObject.DalObject.distance(lat1, long1, lat2, long2);
            double battery = BatteryByKM(weight, KM);

            return battery;
        }

        private double BatteryByKM(int weight, double KM) // חישוב צריכת חשמל לקילומטר
        {

            double power;
            if (weight == 0) power = PowerLightDrone;
            if (weight == 1) power = PowerMediumDrone;
            if (weight == 2) power = PowerHeavyDrone;
            else power = PowerVacantDrone;
            double temp = (KM * power);
            return temp;
        }
    }






}




