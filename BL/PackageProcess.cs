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
                result += $"Shipping Package Shipping Id is {Id},\n";
                result += $"Shipping Status is {PackageShipmentStatus},\n";
                result += $"Shipping Package Priority is {Priority},\n";
                result += $"Shipping Package Weight is {Weight},\n";
                result += $"Shipping Package Sender info:\n {Sender}\n";
                result += $"Shipping Package Receiver info:\n {Receiver}\n";
                result += $"Package Picked Up Location:\n {CollectLocation}\n";
                result += $"Package Destination Location:\n {DestinationLocation}\n";
                result += $"Overall Distance is {Distance},\n";

                return result;
            }
        }
    }

}
