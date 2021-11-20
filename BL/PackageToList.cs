using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class PackageToList
        {
            public int Id { get; set; }
            public string Sender { get; set; }
            public string Receiver { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public PackageStatus Status { get; set; }
            

            public override string ToString()
            {
                string result = "";
                result += $"Id is {Id},\n";
                result += $"Sender Name is {Sender},\n";
                result += $"Receiver Name is {Receiver},\n";
                result += $"WeightCategories is {Weight},\n";
                result += $"Priorities is {Priority},\n";
                result += $"PackageStatus is {Status},\n";


                return result;
            }


        }

    }

}
