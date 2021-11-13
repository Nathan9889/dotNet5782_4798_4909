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
                if (tempDistance < distance && station.ChargeSlots > 0)
                {
                    distance = tempDistance;
                    tempLocation.Latitude = station.Latitude;
                    tempLocation.Longitude = station.Longitude;
                }

            }
            // אם אין לאף אחד עמדות פנויות לזרוק חריגה ולהוסיף עמדות פנויות בתפיסה
            try
            {
                if (distance == int.MaxValue) throw new IBL.BO.Exceptions.StationException("There are no charging slots available at any station");
            }
            catch (Exception ex)
            {
                // הוספת עמדות פנויות בכל התחנות
            }
            

            return tempLocation;
        }

    }


}
