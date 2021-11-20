using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
   namespace BO
    {
        public class DroneToList
        {
            public int ID { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public double Battery { get; set; }
            public DroneStatus Status { get; set; }
            public Location DroneLocation { get; set; }
            public int PackageID { get; set; }

            public override string ToString()
            {
                string result = "";
                result += $"ID is {ID},\n";
                result += $"DroneModel is {Model},\n";
                result += $"MaxWeight is {MaxWeight},\n";
                result += $"Battery is {Battery},\n";
                result += $"Status is {Status},\n";
                result += $"DroneLocation is:\n{DroneLocation}";
                result += $"PackageID num is {PackageID},\n";

                return result;
            }
        }
    }
}
