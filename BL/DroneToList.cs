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
                result += $"Drone ID is {ID}, ";
                result += $"Drone Model is {Model}, ";
                result += $"Drone Max Weight Capacity is {MaxWeight}, ";
                result += $"Drone Battery is {Battery}, ";
                result += $"Drone Status is {Status}, ";
                result += $"Drone Location is: {DroneLocation}\n";
                result += $"Drone PackageID Id is {PackageID}.";

                return result;
            }

            //public override string ToString()
            //{
            //    string result = "";
            //    result += $"Drone ID is {ID},\n";
            //    result += $"Drone Model is {Model},\n";
            //    result += $"Drone Max Weight Capacity is {MaxWeight},\n";
            //    result += $"Drone Battery is {Battery},\n";
            //    result += $"Drone Status is {Status},\n";
            //    result += $"Drone Location is:\n{DroneLocation}";
            //    result += $"Drone PackageID Id is {PackageID}.\n";

            //    return result;
            //}
        }
    }
}
