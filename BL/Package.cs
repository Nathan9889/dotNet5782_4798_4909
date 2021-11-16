using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{

    namespace BO
    {
        public class Package
        {
            public int ID { get; set; }
            public ClientPackage SenderClient { get; set; }
            public ClientPackage ReceiverClient { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DroneWithPackage DroneOfPackage { get; set; }
            public DateTime Created { get; set; }
            public DateTime Associated { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }

            public override string ToString()
            {
                string result = "";
                result += $"ID is {ID}, \n";
                result += $"SenderClient is {SenderClient},\n";
                result += $"ReceiverClient is {ReceiverClient}, \n";
                result += $"Weight is {Weight}, \n";
                result += $"Priority is {Priority}, \n";
                result += $"DroneOfPackage is {DroneOfPackage}, \n";
                result += $"Created at {Created}, \n";
                result += $"Associated at  {Associated}, \n";
                result += $"PickedUp at {PickedUp}, \n";
                result += $"Delivered at {Delivered}, \n";

                return result;
            }

        }
    }
}