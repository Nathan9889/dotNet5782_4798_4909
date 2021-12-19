using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BO
{
    public class ChargingDrone  //רחפן בטעינה
    {
        public int ID { get; set; }
        public double Battery { get; set; }
        public override string ToString()
        {
            string result = "";
            result += $"Charging Drone Id is {ID},\n";
            result += $"Charging Drone Battery is {Battery}.\n";

            return result;
        }
    }
}


