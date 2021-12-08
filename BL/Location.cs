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
        public class Location  //מיקום
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }

            public override string ToString()
            {
                string result = "";
                result += $"{DalObject.DalObject.ConvertLatitude(Latitude)}, ";
                result += $"{DalObject.DalObject.ConvertLongitude(Longitude)}";
                return result;
            }

            //public override string ToString()
            //{
            //    string result = "";
            //    result += $"Latitude is {DalObject.DalObject.ConvertLatitude(Latitude)},\n";
            //    result += $"Longitude is {DalObject.DalObject.ConvertLongitude(Longitude)}.\n";
            //    return result;
            //}
        }
    }

}
