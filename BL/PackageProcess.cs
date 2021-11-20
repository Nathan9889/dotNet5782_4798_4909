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
        public class PackageProcess  //חבילה בהעברה 
        {
            public int Id { get; set; }
            public ShipmentStatus PackageShipmentStatus { get; set; }
            public Priorities Priority { get; set; }
            public WeightCategories Weight { get; set; }
            public ClientPackage Sender { get; set; }
            public ClientPackage Receiver { get; set; }

            public Location CollectLocation { get; set; }
            public Location DestinationLocation { get; set; }

            public double Distance { get; set; }  //check


            public override string ToString()
            {
                string result = "";
                result += $"PackagProcessId is {Id},\n";
                result += $"ShipmentStatus is {PackageShipmentStatus},\n";
                result += $"Priority is {Priority},\n";
                result += $"Weight is {Weight},\n";
                result += $"Sender:\n {Sender}\n";
                result += $"Receiver:\n {Receiver}\n";
                result += $"Collect Location:\n {CollectLocation}\n";
                result += $"Destination Location:\n {DestinationLocation}\n";
                result += $"Distance is {Distance},\n";

                return result;
            }
        }
    }

}
