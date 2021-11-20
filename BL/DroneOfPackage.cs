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
        public class DroneOfPackage  //  רחפן בחבילה
        {
            public int Id { get; set; }
            public double Battery { get; set; }
            public Location CurrentLocation { get; set; }

            public override string ToString()
            {
                string result = "";
                result += $"DroneWithPackageId is {Id},\n";
                result += $"DroneWithPackageBattery is {Battery},\n";
                result += $"DroneWithPackageCurrentLocation:\n {CurrentLocation}\n";

                return result;
            }
        }
    }

}
