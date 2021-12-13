using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                result += $"Package Id is {Id},\n";
                result += $"Package Sender Name is {Sender},\n";
                result += $"Package Receiver Name is {Receiver},\n";
                result += $"Package Weight Capacity is {Weight},\n";
                result += $"Package Priorities is {Priority},\n";
                result += $"Package Status is {Status},\n";

                return result;
            }
        }
    }
