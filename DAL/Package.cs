using System;


namespace IDAL
{
    namespace DO
    {
        public struct Package
        {
           
            public int ID { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public int DroneId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public datetime Requested { get; set; }
            public datetime Scheduled { get; set; }
            public datetime PickedUp { get; set; }
            public datetime Delivered { get; set; }

        }

    }

}