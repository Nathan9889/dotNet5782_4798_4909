using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class PL
    {

        static IEnumerable<BO.DroneToList> drones = new List<BO.DroneToList>();



        public IEnumerable<BO.DroneToList> GetDroneList()
        {
            return BL.DisplayDroneList();
        }

        public BO.Drone GetDrone(int id)
        {
            return BL.DisplayDrone(id);
        }

        public void DeleteDrone(int id)
        {
            BL.DeleteDrone(id);
        }

        public void UpdateDroneName(int id, string name)
        {

            BL.UpdateDroneName(id, name);

        }


        public void UpdateStationNumCharge(int id, int num)
        {
            BL.UpdateStationNumCharge(id, num);

        }


        public void AddDrone(BO.Drone drone)
        {
            BL.AddDrone(drone,10);
        }



        public void ChargeDrone(int id)
        {
            BL.ChargeDrone(id);
        }

        public void FinishCharging(int id)
        {
            BL.FinishCharging(id);
        }


        public void packageToDrone(int id)
        {
            BL.packageToDrone(id);

        }


    }


}



