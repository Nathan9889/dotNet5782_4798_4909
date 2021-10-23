using System;

namespace IDAL
{
    namespace DO
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

        public enum DroneModel
        {
            Dji_Mavic_2_Pro ,Dji_Mavic_2_Air, Dji_Mavic_2_Zoom, Dji_FPV_Combo
        }

    }
}