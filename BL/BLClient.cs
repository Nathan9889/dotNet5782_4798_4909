using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BL
{
    public partial class BL :IBL.IBL
    {










        Location NearestStationToClient(int ClientID) //  חישוב התחנה הקרובה ללקוח
        {
            Location tempLocation = new Location();
            double distance = int.MaxValue;
            foreach (var station in dal.StationsList())
            {
                double tempDistance = DalObject.DalObject.distance(dal.ClientById(ClientID).Latitude, dal.ClientById(ClientID).Longitude, station.Latitude, station.Longitude);
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    tempLocation.Latitude = station.Latitude;
                    tempLocation.Longitude = station.Longitude;
                }

            }
            return tempLocation;
        }

    }


}
