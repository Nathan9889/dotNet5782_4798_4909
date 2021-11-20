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
        public class PackageAtClient  //חבילה אצל לקוח 
        {
            public int Id { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public PackageStatus Status { get; set; }
            public ClientPackage Source_Destination { get; set; }

            public override string ToString()
            {
                string result = "";
                result += $"Id is {Id},\n";
                result += $"Weight is {Weight},\n";
                result += $"Priority is {Priority},\n";
                result += $"Status is {Status},\n";
                result += $"Source_Destination:\n {Source_Destination}\n";
                return result;
            }
        }
    }
}
