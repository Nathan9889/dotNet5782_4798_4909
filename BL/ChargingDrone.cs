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
        public class ChargingDrone  //רחפן בטעינה
        {
            public int ID { get; set; }
            public double Battery { get; set; }
            public override string ToString()
            {
                string result = "";
                result += $"ChargingDroneId is {ID},\n";
                result += $"ChargingDroneBattery is {Battery},\n";

                return result;
            }
        }
    }

}
