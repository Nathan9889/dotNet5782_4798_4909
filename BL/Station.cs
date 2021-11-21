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
                result += $"Station ID is {ID}, \n";
                result += $"Station Name is {Name},\n";
                result += $"Number of Available ChargeSlots of the Station is {AvailableChargeSlots}, \n";
                result += $"Station Location:\n {StationLocation},\n";
                if(ChargingDronesList.Count() > 0)
                {
                    result += $"List of charging Drones of the station";
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



