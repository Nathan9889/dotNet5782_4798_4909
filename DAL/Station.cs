using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            result += $"Station Latitude is {DalObject.Coordinates.ConvertLatitude(Latitude)}, \n";
            result += $"Station Longitude is {DalObject.Coordinates.ConvertLongitude(Longitude)}, \n";

            return result;
        }
    }
}

