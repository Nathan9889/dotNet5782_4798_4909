using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace DO
    {
        public struct Drone
        {
            public int ID { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }

            //public DroneStatus Status { get; set; }
            //public double Battery { get; set; }


            public override string ToString()
            {
                string result = "";
                result += $"Drone ID is {ID}, \n";
                result += $"Drone Model name is {Model},\n";
                result += $"Drone MaxWeight is {MaxWeight}, \n";
                
                return result;
            }
        }
    }

