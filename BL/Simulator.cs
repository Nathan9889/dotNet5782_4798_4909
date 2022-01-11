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
        BlApi.IBL BL;
        int droneID;
        Action<string,int> action;
        Func<bool> stop;

        Random random = new Random();
        private int DELAY = 1000;
        private int SPEED = 1;
       
        public Simulator(BlApi.IBL bl, int id, Action<string,int> action, Func<bool> stop)
        {
            BL = bl;
            droneID = id;
            this.action = action;
            this.stop = stop;
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Drone drone;
            DateTime? startCharging = null;
            
            while (stop.Invoke() == false)
            {
                drone = BL.DisplayDrone(id);

                switch (drone.Status)
                {
                    case DroneStatus.Available:

                        try
                        {
                            BL.packageToDrone(id);
                            Thread.Sleep(DELAY);
                            action("Associate",0);
                        }
                        catch (Exception ex)
                        {
                            if(ex.Message == "There is no package for the Drone")
                            {
                                var allPackages = BL.DisplayPackageListWithoutDrone();

                                var Packages = (from p in allPackages
                                        where (int)p.Weight <= (int)drone.MaxWeight
                                         select p);

                                if (Packages.Count() > 0) // האם יש חבילות שהוא יכול לקחת ורק יש בעיה בסוללה
                                {
                                    BL.ChargeDrone(id);
                                    Thread.Sleep(DELAY);
                                    action("charging",0);
                                }
                                else        // אין חבילות-  שיתווסף חבילות
                                {
                                   
                                    action("No packages",0);
                                    Thread.Sleep(3 * DELAY);
                                }
                            }
                        }
                        break;

                    case DroneStatus.Maintenance:
                        if (drone.Battery == 100)
                        {
                            int i =0;
                            foreach (var item in bl.DisplayStationList())
                            {
                                i = item.ID;
                                if (bl.GetStationWithDrones(item.ID).ChargingDronesList.Any(d => d.ID == drone.ID) == true) break; 
                            }
                            bl.FinishCharging(id);
                            startCharging = null;

                            action("Finish charging",i);
                            Thread.Sleep(DELAY);
                            break;
                        }
                        BL.updateDroneBattery(drone.ID, startCharging);

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

                                    if (drone.DronePackageProcess.Distance > 1)
                                    {
                                        BL.UpdateDroneLocation(drone.ID, lonPlus, latPlus);
                                        BL.UpdateLessBattery(drone.ID, BL.BatteryByKM(-1, 1 * SPEED));
                                    }
                                    else break;

                                    drone = BL.DisplayDrone(drone.ID);
                                    action("Battery and location",0);
                                }

                                BL.PickedUpByDrone(drone.ID);
                                action("PickedUp",0);
                                break;


                            case ShipmentStatus.OnGoing:

                                latMinusLat = drone.DronePackageProcess.DestinationLocation.Latitude - drone.DronePackageProcess.CollectLocation.Latitude;
                                lonMinusLon = drone.DronePackageProcess.DestinationLocation.Longitude - drone.DronePackageProcess.CollectLocation.Longitude;

                                latPlus = latMinusLat / drone.DronePackageProcess.Distance * SPEED;
                                lonPlus = lonMinusLon / drone.DronePackageProcess.Distance * SPEED;

                                while (drone.DronePackageProcess.Distance > 0 )
                                {
                                    Thread.Sleep(DELAY);

                                    if (drone.DronePackageProcess.Distance > 1)
                                    {
                                        BL.UpdateDroneLocation(drone.ID, lonPlus, latPlus);
                                        BL.UpdateLessBattery(drone.ID, BL.BatteryByKM((int)drone.DronePackageProcess.Weight, 1 * SPEED));
                                    }
                                    else break;

                                    drone = BL.DisplayDrone(drone.ID);
                                    action("Battery and location",0);
                                }
                                
                                BL.DeliveredToClient(drone.ID);
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
