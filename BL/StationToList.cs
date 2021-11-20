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
            public int AvailableChargingSlots { get; set; }
            public int BusyChargingSlots { get; set; }
           

            public override string ToString()
            {
                string result = "";
                result += $"ID is {ID},\n";
                result += $"Name is {Name},\n";
                result += $"num of AvailableChargingSlots is {AvailableChargingSlots},\n";
                result += $"num of BusyChargingSlots is {BusyChargingSlots},\n";

                return result;
            }
        }
    }
}
