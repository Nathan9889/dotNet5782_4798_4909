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

        public Client GetClient(int id)
        {
            Client client = default;
            try
            {
                IDAL.DO.Client dalClient = dal.ClientById(id);

            }
            catch (IDAL.DO.Exceptions.IDException ClientEx)
            {
                throw new IBL.BO.Exceptions.BLClientException($"Client ID {id} not found", ClientEx);
            }

            return client;
        }

        IDAL.DO.Station NearestStationToClient(int ClientID) //  חישוב התחנה הקרובה ללקוח
        {
            IDAL.DO.Station tempStation = new IDAL.DO.Station();
            double distance = int.MaxValue;
            if (dal.StationWithCharging().Count() == 0) throw new IBL.BO.Exceptions.SendingDroneToCharging("There are no charging slots available at any station"); // אם אין עמדות טעינה פנויות באף תחנה

            foreach (var station in dal.StationWithCharging())
            {
                double tempDistance = DalObject.DalObject.distance(dal.ClientById(ClientID).Latitude, dal.ClientById(ClientID).Longitude, station.Latitude, station.Longitude);
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    tempStation.Latitude = station.Latitude;
                    tempStation.Longitude = station.Longitude;
                }

            }

            return tempStation;
        }

    }
}
