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
                result += $"ID is {ID}, \n";
                result += $"Name is {Name},\n";
                result += $"ChargeSlots is {ChargeSlots}, \n";
                result += $"Latitude is {Latitude}, \n";
                result += $"Longitude is {Longitude}, \n";

                return result;
            }
        }
    }
}
