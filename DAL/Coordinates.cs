using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class Coordinates
    {

        /// <summary>
        /// The function Convert from Decimal to Sexagesimal and returns the result
        /// </summary>
        /// <param name="cor"></param>
        /// <returns></returns>
        private static string ConvertCoordinates(double cor)
        {

            cor = Math.Abs(cor);
            string str = Convert.ToString((int)cor) + "°";
            cor -= (int)cor;
            cor *= 60;
            str += Convert.ToString((int)cor) + "'";
            cor -= (int)cor;
            cor *= 60;
            str += Convert.ToString((int)cor) + ".";
            cor -= (int)cor;
            cor *= 1000;
            str += Convert.ToString((int)cor) + "''";
            return str;
        }

        /// <summary>
        /// The func Calculate the distance between Two Coordination and returns it
        /// </summary>
        public static double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            if (double.IsNaN(dist)) return 0;
            return dist * 1.609344;
        }

        public static string ConvertLongitude(double longitude)
        {
            string str = ConvertCoordinates(longitude);
            if (longitude < 0) return (str += "W");
            return (str += "E");
        }
        public static string ConvertLatitude(double Latitude)
        {
            string str = ConvertCoordinates(Latitude);
            if (Latitude < 0) return (str += "S");
            return (str += "N");
        }


    }
}
