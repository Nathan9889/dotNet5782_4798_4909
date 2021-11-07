using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class StationToList
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int VacantChargingStand { get; set; }
            public int TakenChargingStand { get; set; }
           

            public override string ToString()
            {
                string result = "";
                result += $"PackagProcessId is {ID},\n";
                result += $"Name is {Name},\n";
                result += $"num of VacantChargingStand is {VacantChargingStand},\n";
                result += $"num of TakenChargingStand is {TakenChargingStand},\n";

                return result;
            }
        }
    }
}
