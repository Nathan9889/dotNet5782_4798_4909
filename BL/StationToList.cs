using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            result += $"Station ID is {ID},\n";
            result += $"Station Name is {Name},\n";
            result += $"Number of Available Charging Slots is {AvailableChargingSlots},\n";
            result += $"Number of Occupied ChargingSlots is {BusyChargingSlots},\n";

            return result;
        }
    }
}

