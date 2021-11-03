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
        public class DroneWithPackage  //  רחפן בחבילה
        {
            public int Id { get; set; }
            public int Battery { get; set; }
            public Location CurrentLocation { get; set; }

            public override string ToString()
            {
                string result = "";
                result += $"DroneWithPackageId is {Id},\n";
                result += $"DroneWithPackageBattery is {Battery},\n";
                result += $"DroneWithPackageCurrentLocation is {CurrentLocation},\n";

                return result;
            }
        }
    }

}
