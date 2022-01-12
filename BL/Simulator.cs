using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Threading;
using static BL.BL;
using System.Diagnostics;

namespace BL
{
    class Simulator
    {
        BL BL;
        int droneID;
        Action<string,int> action;
        Func<bool> stop;

        Random random = new Random();
        private int DELAY = 1000;
        private int SPEED = 1;
       
        public Simulator(BL bl, int id, Action<string,int> action, Func<bool> stop)
        {
            BL = bl;
            droneID = id;
            this.action = action;
            this.stop = stop;
            
            

            Drone drone;
            DateTime? startCharging = null;
            
            while (stop.Invoke() == false)
            {
                lock (BL)
                {
                    drone = BL.DisplayDrone(id);
                }

                switch (drone.Status)
                {

                    case DroneStatus.Available:

                        try
                        {
                            lock (BL)
                            {
                                BL.packageToDrone(id);
                                drone = BL.DisplayDrone(id);
                            }
                            Thread.Sleep(DELAY);
                            action("Associate",drone.DronePackageProcess.Id);
                        }
                        catch (Exception ex)
                        {
                            if(ex.Message == "There is no package for the Drone")
                            {
                                IEnumerable<PackageToList> allPackages;
                                lock (BL)
                                {
                                    allPackages = BL.DisplayPackageListWithoutDrone();
                                }
                                var Packages = (from p in allPackages
                                                where (int)p.Weight <= (int)drone.MaxWeight
                                                select p);

                                if (Packages.Count() > 0) // האם יש חבילות שהוא יכול לקחת ורק יש בעיה בסוללה
                                {
                                    lock (BL)
                                    {
                                        try
                                        {
                                            BL.ChargeDrone(id);
                                        }
                                        catch (Exception exe) // אם אין עמדות טעינה פנויות בתחנה הקרובה או שאין בכלל תחנות עם עמדות פנויות נחכה קצת וננסה שוב
                                        {
                                            if(exe.Message == "There are no available charging stations at the nearest station" || ex.Message == "The drone can not reach the station, Not enough battery")
                                            Thread.Sleep(8*DELAY);
                                        }
                                    }
                                    Thread.Sleep(DELAY);

                                    int i = 0;
                                    foreach (var item in BL.DisplayStationList())
                                    {
                                        i = item.ID;
                                        if (BL.GetStationWithDrones(item.ID).ChargingDronesList.Any(d => d.ID == drone.ID) == true) break;
                                    }
                                    action("charging", i);

                                }
                                else        // אין חבילות-  שיתווסף חבילות
                                {

                                    action("No packages", 0);
                                    Thread.Sleep(3 * DELAY);
                                }
                            }
                        }
                        break;




                    case DroneStatus.Maintenance:


                        lock (BL)
                        {
                            if (drone.Battery == 100)
                            {
                                int i = 0;
                                foreach (var item in BL.DisplayStationList())
                                {
                                    i = item.ID;
                                    if (BL.GetStationWithDrones(item.ID).ChargingDronesList.Any(d => d.ID == drone.ID) == true) break;
                                }
                                BL.FinishCharging(id);
                                startCharging = null;

                                action("Finish charging", i);
                                Thread.Sleep(DELAY);
                                break;
                            }
                            BL.updateDroneBattery(drone.ID, startCharging);
                        }

                        startCharging = DateTime.Now;  
                        action("Battery and location",0);
                        Thread.Sleep(DELAY);
                        break;



                    case DroneStatus.Shipping:


                        double lonPlus, latPlus, lonMinusLon, latMinusLat;
                        Package package = BL.DisplayPackage(drone.DronePackageProcess.Id);

                        switch (drone.DronePackageProcess.PackageShipmentStatus)
                        {
                            case ShipmentStatus.Waiting:

                                latMinusLat = drone.DronePackageProcess.CollectLocation.Latitude - drone.DroneLocation.Latitude;
                                lonMinusLon = drone.DronePackageProcess.CollectLocation.Longitude - drone.DroneLocation.Longitude;

                                latPlus = latMinusLat / drone.DronePackageProcess.Distance * SPEED;
                                lonPlus = lonMinusLon / drone.DronePackageProcess.Distance * SPEED;

                                while (drone.DronePackageProcess.Distance > 0 )
                                {
                                    Thread.Sleep(DELAY);

                                    lock (BL)
                                    {
                                        if (drone.DronePackageProcess.Distance > 1)
                                        {
                                            BL.UpdateDroneLocation(drone.ID, lonPlus, latPlus);
                                            BL.UpdateLessBattery(drone.ID, BL.BatteryByKM(-1, 1 * SPEED));
                                        }
                                        else break;

                                        drone = BL.DisplayDrone(drone.ID);
                                    }
                                    action("Battery and location",0);
                                }

                                lock (BL)
                                {
                                    BL.PickedUpByDrone(drone.ID);
                                }
                                action("PickedUp",drone.DronePackageProcess.Id);
                                break;


                            case ShipmentStatus.OnGoing:

                                latMinusLat = drone.DronePackageProcess.DestinationLocation.Latitude - drone.DronePackageProcess.CollectLocation.Latitude;
                                lonMinusLon = drone.DronePackageProcess.DestinationLocation.Longitude - drone.DronePackageProcess.CollectLocation.Longitude;

                                latPlus = latMinusLat / drone.DronePackageProcess.Distance * SPEED;
                                lonPlus = lonMinusLon / drone.DronePackageProcess.Distance * SPEED;

                                while (drone.DronePackageProcess.Distance > 0 )
                                {
                                    Thread.Sleep(DELAY);

                                    lock (BL)
                                    {
                                        if (drone.DronePackageProcess.Distance > 1)
                                        {
                                            BL.UpdateDroneLocation(drone.ID, lonPlus, latPlus);
                                            BL.UpdateLessBattery(drone.ID, BL.BatteryByKM((int)drone.DronePackageProcess.Weight, 1 * SPEED));
                                        }
                                        else break;

                                        drone = BL.DisplayDrone(drone.ID);
                                    }
                                    action("Battery and location",0);
                                }

                                lock (BL)
                                {
                                    BL.DeliveredToClient(drone.ID);
                                }
                                action("Delivered", drone.DronePackageProcess.Id);
                                Thread.Sleep(DELAY);

                                break;
                            
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        

       


    }
}
