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
            public DateTime Created { get; set; }
            public DateTime Associated { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }


            public override string ToString()
            {
                string result = "";
                result += $"Package ID is {ID}, \n";
                result += $"Package SenderId is {SenderId},\n";
                result += $"Package TargetId is {TargetId}, \n";
                result += $"Package DroneId is {DroneId}, \n";
                result += $"Package Weight is {Weight}, \n";
                result += $"Package Priority is {Priority}, \n";
                result += $"Package Created at {Created}, \n";
                result += $"Package Associated at  {Associated}, \n";
                result += $"Package PickedUp at {PickedUp}, \n";
                result += $"Package Delivered at {Delivered}, \n";

                return result;
            }

        }

    }

}