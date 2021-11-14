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
        public List<DroneToList> DroneList;
        public IDAL.IDAL dal;
        public double PowerVacantDrone;
        public double PowerLightDrone;
        public double PowerMediumDrone;
        public double PowerHeavyDrone;
        public double ChargeRate;


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

                        int minBattery;
                        double KM = DalObject.DalObject.distance(droneToList.DroneLocation.Latitude, droneToList.DroneLocation.Longitude, targetLocation.Latitude, targetLocation.Longitude);
                        minBattery = BatteryByKM((int)package.Weight, KM);

                        IDAL.DO.Station stationLocation = NearestStationToClient(package.TargetId);
                        KM = DalObject.DalObject.distance(targetLocation.Latitude, targetLocation.Longitude, stationLocation.Latitude, stationLocation.Longitude);
                        minBattery += BatteryByKM(3, KM);

                        if (minBattery > 100) throw new Exception();
                        droneToList.Battery = rand.Next(minBattery, 101); // בין הצריכה המינמלית ל100

                        break;
                    }
                }

                if (!flag)
                {
                    droneToList.Status = (DroneStatus)(rand.Next(1, 3));
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


                        int minBattery;
                        IDAL.DO.Station stationLocation = NearestStationToClient(dal.ClientById(index).ID);
                        double KM = DalObject.DalObject.distance(droneToList.DroneLocation.Latitude, droneToList.DroneLocation.Longitude, stationLocation.Latitude, stationLocation.Longitude);
                        minBattery = BatteryByKM(3, KM);
                        droneToList.Battery = rand.Next(minBattery, 101);

                    }
                }

                DroneList.Add(droneToList);
            }
        }


        void IBL.IBL.AddDrone(Drone drone, int stationNumToCharge)
        {
            try // חריגה מהשכבה הלוגית
            {
                if (drone.ID < 0) throw new IBL.BO.Exceptions.IDException("Drone ID can not be negative", drone.ID);
                if (!dal.StationsList().Any(x => x.ID == stationNumToCharge)) throw new IBL.BO.Exceptions.IDException("Station ID not found", stationNumToCharge);
                if (dal.StationById(stationNumToCharge).ChargeSlots <= 0) throw new IBL.BO.Exceptions.StationException("There are no charging slots available at the station", stationNumToCharge);
            }
            catch (IBL.BO.Exceptions.IDException ex)
            {
                if (ex.Message == "Drone ID can not be negative") { throw; }
                else if (ex.Message == "Station ID not found") { throw; }
            }
            catch (IBL.BO.Exceptions.StationException ex) { throw; }


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



        void IBL.IBL.UpdateDroneName(Drone drone)
        {
            IDAL.DO.Drone droneDAL;
            try
            {
                if (!DroneList.Any(x => x.ID == drone.ID)) throw new IBL.BO.Exceptions.IDException("Drone ID not found", drone.ID); // // חריגה משהכבה הלוגית
                droneDAL = dal.DroneById(drone.ID); // חריגה משכבת הנתונים
            }
            catch (IBL.BO.Exceptions.IDException ex) { throw; }
            catch (IDAL.DO.Exceptions.IDException ex) { throw; }

            foreach (var droneBL in DroneList) // עדכון ברשימה ב BL
            {
                if (droneBL.ID == drone.ID)
                {
                    droneBL.Model = drone.Model;
                    break;
                }
            }


            IDAL.DO.Drone droneDalTemp = droneDAL; // עדכון ברשימה בשכבת התונים
            droneDalTemp.Model = drone.Model;
            try // לא אמור להיות חריגה כי כבר בדקנו בתחילת הפונקציה שזה קיים
            {
                dal.DeleteDrone(droneDAL);
                dal.AddDrone(droneDAL);
            }
            catch (IDAL.DO.Exceptions.IDException ex) { throw new IBL.BO.Exceptions.IDException("Fault in drone update. Was not supposed to be an exception because we have already checked before", ex, drone.ID); }
        }



        void IBL.IBL.ChargeDrone(int ID)
        {
            DroneToList drone = DroneList.Find(drone => drone.ID == ID);
            if (drone == null) {throw new IBL.BO.Exceptions.IDException("Drone ID not found", ID);}

            if (drone.Status == DroneStatus.Available)
            {
                IDAL.DO.Station station = NearestStationToDrone(drone.ID);
                double minBattery = batteryConsumption(drone.DroneLocation.Latitude, drone.DroneLocation.Longitude, station.Latitude, station.Longitude,4); // כמות הסוללה הנדרשת כדי שהרחפן יוכל לטוס ממיקומו ועד התחנה

                if (drone.Battery < minBattery) throw new IBL.BO.Exceptions.SendingDroneToCharging("The drone can not reach the station, Not enough battery");

                // ניתן לשלוח לטעינה
                DroneToList tempDrone = drone;

                tempDrone.Battery = tempDrone.Battery - minBattery ; // הרחפן כביכול נסע לתחנה ולכן צריך להוריד ממנו את הסוללה של הנסיעה מהמיקום שלו לתחנה
                tempDrone.DroneLocation.Latitude = station.Latitude;
                tempDrone.DroneLocation.Longitude = station.Longitude;
                tempDrone.Status = DroneStatus.Maintenance;

                DroneList.Add(tempDrone); // עדכון ברשימה בשכבה 2
                DroneList.Remove(drone);

                dal.DroneCharge(dal.DroneById(drone.ID) , station.ID); // יצירת מופע של רחפן בטעינה והקטנת מספר עמדות הטעינה

            }


        }


      


        private IDAL.DO.Station NearestStationToDrone(int DroneID) // חישוב התחנה הקרובה לרחפן עם עמדות פנויות והחזרה שלה
        {
            DroneToList drone = DroneList.Find(x => x.ID == DroneID);
            IDAL.DO.Station tempLocation = new IDAL.DO.Station();
            double distance = int.MaxValue;

            if (dal.StationWithCharging().Count() == 0) throw new IBL.BO.Exceptions.SendingDroneToCharging("There are no charging slots available at any station"); // אם אין עמדות טעינה פנויות באף תחנה
            foreach (var station in dal.StationWithCharging())
            {

                double tempDistance = DalObject.DalObject.distance(drone.DroneLocation.Latitude, drone.DroneLocation.Longitude, station.Latitude, station.Longitude);
                if (tempDistance < distance )
                {
                    distance = tempDistance;
                    tempLocation.Latitude = station.Latitude;
                    tempLocation.Longitude = station.Longitude;
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

        int BatteryByKM(int weight, double KM) // חישוב צריכת חשמל לקילומטר
        {

            double power;
            if (weight == 0) power = PowerLightDrone;
            if (weight == 1) power = PowerMediumDrone;
            if (weight == 2) power = PowerHeavyDrone;
            else power = PowerVacantDrone;
            int temp = (int)(KM * power);
            return temp;
        }
    }






}






