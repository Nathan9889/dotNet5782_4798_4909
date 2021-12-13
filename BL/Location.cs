using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


    namespace BO
    {
        public class Location  //מיקום
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }

            public override string ToString()
            {
                string result = "";
                result += $"{DalObject.Coordinates.ConvertLatitude(Latitude)}\n";
                result += $"{DalObject.Coordinates.ConvertLongitude(Longitude)}";
                return result;
            }

        }
    }


