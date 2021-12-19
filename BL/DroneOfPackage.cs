using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BO
{
    public class DroneOfPackage  //  רחפן בחבילה
    {
        public int Id { get; set; }
        public double Battery { get; set; }
        public Location CurrentLocation { get; set; }

        public override string ToString()
        {
            string result = "";
            result += $"Drone With Package Id is {Id},\n";
            result += $"Drone With Package Battery is {Battery},\n";
            result += $"Drone With Package Current Location:\n {CurrentLocation}\n";

            return result;
        }
    }
}

