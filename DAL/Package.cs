using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public DateTime Requested { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }



            public override string ToString()
            {
                string result = "";
                result += $"ID is {ID}, \n";
                result += $"SenderId is {SenderId},\n";
                result += $"TargetId is {TargetId}, \n";
                result += $"DroneId is {DroneId}, \n";
                result += $"Weight is {Weight}, \n";
                result += $"Priority is {Priority}, \n";
                result += $"Requested :  {Requested}, \n";
                result += $"Scheduled at {Scheduled}, \n";
                result += $"PickedUp at {PickedUp}, \n";
                result += $"Delivered at {Delivered}, \n";


                return result;
            }
        }

    }

}