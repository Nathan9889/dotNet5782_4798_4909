using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int ChargeSlots { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }


            public override string ToString()
            {
                string result = "";
                result += $"Station ID is {ID}, \n";
                result += $"Station Name is {Name},\n";
                result += $"Number of ChargeSlots is {ChargeSlots}, \n";
                result += $"Station Latitude is {DalObject.DalObject.ConvertLatitude(Latitude)}, \n";
                result += $"Station Longitude is {DalObject.DalObject.ConvertLongitude(Longitude)}, \n";

                return result;
            }
        }
    }
}
