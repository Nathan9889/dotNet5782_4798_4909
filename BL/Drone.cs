using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

    namespace BO
    {
        public class Drone
        {
            public int ID { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public double Battery { get; set; }
            public DroneStatus Status { get; set; }
            public PackageProcess DronePackageProcess { get; set; }
            public Location DroneLocation { get; set; }


            public override string ToString()
            {
                string result = "";
                result += $"Drone ID is {ID}, \n";
                result += $"Drone Model Name Is {Model},\n";
                result += $"Drone Max Weight Capacity is {MaxWeight}, \n";
                result += $"Drone Battery is {Battery}, \n";
                result += $"Drone Status is {Status}, \n";
                if(DronePackageProcess != null) result += $"Package Shipping of drone:\n{DronePackageProcess},\n";
                result += $"Drone Location is:\n{DroneLocation}";
     
                return result;
            }
        }
    }

