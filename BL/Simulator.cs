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

        private int DELAY = 1000;
        private int KM = 1;

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
                                else// אין חבילות- לולאה עד שיתווסף חבילות
                                {

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
                        }
                            
                        
                        //BL.updateDroneBattary(id,startCharging);
                        action();
                        Thread.Sleep(DELAY);
                        break;


                    case DroneStatus.Shipping:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
