using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
namespace BL
{
    public partial class BL : IBL.IBL
    {

        public Station GetStation(int id)
        {
            Station station = default;
            try
            {
                IDAL.DO.Station dalStation = dal.StationById(id);

            }
            catch (IDAL.DO.Exceptions.IDException SationEx)
            {
                throw new IBL.BO.Exceptions.BLStationException($"Sation ID {id} not found", SationEx);
            }

            return station;
        }


        // public List<StationToList> StationList;
        public void AddStation(Station station)
        {
            try
            {
                if (station.ID < 0) throw new IBL.BO.Exceptions.IDException("Station ID can not be negative", station.ID);
                if (!dal.StationsList().Any(x => x.ID == station.ID)) throw new IBL.BO.Exceptions.IDException("Station ID not found", station.ID);
            }
            catch (IBL.BO.Exceptions.IDException ex)
            {
                if (ex.Message == "Station ID can not be negative") { throw; }
                else if (ex.Message == "Station ID not found") { throw; }

            }

            IDAL.DO.Station dalStation = new IDAL.DO.Station();
            dalStation.ID = station.ID;
            dalStation.Name = station.Name;

            if(!( (station.StationLocation.Latitude >= 31.73) && (station.StationLocation.Latitude <= 31.83) ||
                (station.StationLocation.Longitude >= 35.16) && (station.StationLocation.Longitude <= 35.26)) )
            {
                throw new Exceptions.LocationOutOfRange("Station Location entered is out of shipping range", station.ID);
            }
            dalStation.Latitude = station.StationLocation.Latitude;
            dalStation.Longitude = station.StationLocation.Longitude;
            dalStation.ChargeSlots = station.VacantChargeSlots;

            try
            {
                dal.AddStation(dalStation);
            }
            catch (IDAL.DO.Exceptions.IDException ex)
            {
                throw new Exceptions.IDException("Station with this ID already exists", ex, station.ID);
            }

        }

        public void UpdateStation(int id, string name , int numCharge)
        {
            IDAL.DO.Station dalStation;
            if(! dal.StationsList().Any(x => x.ID == id))
                throw new IBL.BO.Exceptions.IDException("Station ID not found", id);
            dalStation = dal.StationById(id);

            IDAL.DO.Station stationTemp = dalStation;

            if(name != "")
                stationTemp.Name = name;
            
            stationTemp.ChargeSlots = numCharge;

            dal.DeleteStation(dalStation);
            dal.AddStation(stationTemp);
                
        }



    }


}
