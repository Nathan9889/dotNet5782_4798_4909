using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class Coordinates
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
        public static double distance(double x1, double y1, double x2, double y2)
        {
            double m = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            return m * 100;
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
