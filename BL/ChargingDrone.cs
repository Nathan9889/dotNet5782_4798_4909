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
        public class ChargingDrone  //משלוח אצל לקוח 
        {
            public int Id { get; set; }
            public int Battery { get; set; }


            public override string ToString()
            {
                string result = "";
                result += $"ChargingDroneId is {Id},\n";
                result += $"ChargingDroneBattery is {Battery},\n";

                return result;
            }
        }
    }

}
