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
        public class Station
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public Location StationLocation { get; set; }
            public int AvailableChargeSlots { get; set; }
            public List<ChargingDrone> ChargingDronesList { get; set; }
            public override string ToString()
            {
                string result = "";
                result += $"ID is {ID}, \n";
                result += $"Name is {Name},\n";
                result += $"Available ChargeSlots is {AvailableChargeSlots}, \n";
                result += $"Station Location:\n {StationLocation},\n";
                if(ChargingDronesList.Count() > 0)
                {
                    foreach (var chargingDrone in ChargingDronesList)
                    {
                        result += $"{chargingDrone}\n";
                    }
                }

                return result;
            }
        }
    }

}



