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
        Action action;
        Func<bool> stop;

        Random random = new Random();
        private int DELAY = 1000;
        private int SPEED = 1;
       
        public Simulator(BlApi.IBL bl, int id, Action action, Func<bool> stop)
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

                        }
                        catch (Exception ex)
                        {
                            if(ex.Message == "There is no package for the Drone")
                            {
                                if (BL.DisplayPackageListWithoutDrone().Count() > 0)
                                {
                                    BL.ChargeDrone(id);
                                   
                                }
                                else        // אין חבילות-  שיתווסף חבילות
                                {
                                    Thread.Sleep(3*DELAY);
                                    addPackages();
                                }
                            }
                        }

                        Thread.Sleep(DELAY);
                        action();
                        break;

                    case DroneStatus.Maintenance:
                        if (drone.Battery == 100)
                        {
                            bl.FinishCharging(id);
                            startCharging = null;
                            action();
                            Thread.Sleep(DELAY);
                            break;
                        }
                        BL.updateDroneBattery(drone.ID, startCharging);

                        startCharging = DateTime.Now;  
                        action();
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
                                    action();
                                }

                                BL.PickedUpByDrone(drone.ID);
                                action();
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
                                    action();
                                }

                                BL.DeliveredToClient(drone.ID);
                                action();
                                Thread.Sleep(DELAY);

                                break;
                            
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        

        void addPackages()
        {
            IEnumerable<ClientToList> clients = BL.DisplayClientList();

            for (int i = 0; i <random.Next(4,8); i++)
            {
                BL.AddPackage(new Package()
                {
                    SenderClient = new ClientPackage() { ID = clients.ElementAt(random.Next(0, clients.Count())).Id },
                    TargetClient = new ClientPackage() { ID = clients.ElementAt(random.Next(0, clients.Count())).Id },
                    Priority = (Priorities) random.Next(0, 3),
                    Weight = (WeightCategories)random.Next(0, 3)
                });
            }
        }


    }
}
