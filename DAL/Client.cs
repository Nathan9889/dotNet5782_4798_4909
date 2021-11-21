using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IDAL
{
     
    namespace DO
    {
        public struct Client
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }


            public override string ToString()
            {
                string result = "";
                result += $"Client Name is {Name},\n";
                result += $"Client ID is {ID}, \n";
                result += $"Client Phone is {Phone.Substring(0,3) + '-' + Phone.Substring(3)}, \n";
                result += $"Client Latitude is {DalObject.DalObject.ConvertLatitude(Latitude)}, \n";
                result += $"Client Longitude is {DalObject.DalObject.ConvertLongitude(Longitude)}, \n";

                return result;
            }
        }
    }
}