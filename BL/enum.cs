using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL
{
    namespace BO
    {

        public enum WeightCategories
        {
            Light, Medium, Heavy
        }


        public enum Priorities
        {
            Standard, Fast, Urgent
        }
        public enum DroneStatus
        {
            Available, Maintenance, Shipping
        }

        public enum ShipmentStatus
        {
            Waiting , OnGoing
        }

        public enum PackageStatus
        {
            Created, Associated, PickedUp, Delivered
        }

        public enum DroneModel
        {
            Dji_Mavic_2_Pro, Dji_Mavic_2_Air, Dji_Mavic_2_Zoom, Dji_FPV_Combo
        }

    }

}



