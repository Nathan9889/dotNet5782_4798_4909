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
        /// <summary>
        /// The function Add a station in the list of station in Datasource 
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            if (station.ID < 0)                                                                         // Id input exceptions
                throw new IBL.BO.Exceptions.IDException("Station ID can not be negative", station.ID);
            if (!dal.StationsList().Any(x => x.ID == station.ID))
                throw new IBL.BO.Exceptions.IDException("Station ID not found", station.ID);


            IDAL.DO.Station dalStation = new IDAL.DO.Station();   //new datasource station then assign
            dalStation.ID = station.ID;
            dalStation.Name = station.Name;
            if(!( (station.StationLocation.Latitude >= 31.73) && (station.StationLocation.Latitude <= 31.83) ||
                (station.StationLocation.Longitude >= 35.16) && (station.StationLocation.Longitude <= 35.26)) )
            {
                throw new Exceptions.LocationOutOfRange("Station Location entered is out of shipping range", station.ID);
            }
            dalStation.Latitude = station.StationLocation.Latitude;
            dalStation.Longitude = station.StationLocation.Longitude;
            dalStation.ChargeSlots = station.AvailableChargeSlots;

            try         //adding the station to staton list in datasource
            {
                dal.AddStation(dalStation);
            }
            catch (IDAL.DO.Exceptions.IDException ex)
            {
                throw new Exceptions.IDException("Station with this ID already exists", ex, station.ID);
            }

        }

        ///// <summary>
        ///// function update an existing client and changes it name or free chargeslot number according to user
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="name"></param>
        ///// <param name="numCharge"></param>
        //public void UpdateStation(int id, string name , int numCharge)
        //{
        //    IDAL.DO.Station dalStation;
        //    if(! dal.StationsList().Any(x => x.ID == id))
        //        throw new IBL.BO.Exceptions.IDException("Station ID not found", id);
        //    dalStation = dal.StationById(id);

        //    IDAL.DO.Station stationTemp = dalStation;

        //    if(name != "")
        //        stationTemp.Name = name;
        //    /// בדיקה על int

        //    int minSlots = 0;
        //    foreach (var item in dal.droneChargesList())
        //    {
        //        if (item.StationId == id) minSlots++;
        //    }
        //    if (minSlots < numCharge) throw new IBL.BO.Exceptions.StationException("The number of new slots can not be less than the number of Drones in charging",id);
        //    stationTemp.ChargeSlots = numCharge;

        //    dal.DeleteStation(dalStation);
        //    dal.AddStation(stationTemp);
                
        //}

        public void UpdateStationName(int id, string name)
        {
            IDAL.DO.Station dalStation;
            if (!dal.StationsList().Any(x => x.ID == id))
                throw new IBL.BO.Exceptions.IDException("Station ID not found", id);
            dalStation = dal.StationById(id);

            IDAL.DO.Station stationTemp = dalStation;
            stationTemp.Name = name;

            dal.DeleteStation(dalStation);
            dal.AddStation(stationTemp);
        }

        public void UpdateStationNumCharge(int id, int numCharge)
        {
            IDAL.DO.Station dalStation;
            if (!dal.StationsList().Any(x => x.ID == id))
                throw new IBL.BO.Exceptions.IDException("Station ID not found", id);
            dalStation = dal.StationById(id);
            IDAL.DO.Station stationTemp = dalStation;

            int minSlots = 0;
            foreach (var item in dal.droneChargesList())
            {
                if (item.StationId == id) minSlots++;
            }
            if (numCharge < minSlots) throw new IBL.BO.Exceptions.StationException("The number of new slots can not be less than the number of Drones in charging.", id);
            stationTemp.ChargeSlots = numCharge;

            dal.DeleteStation(dalStation);
            dal.AddStation(stationTemp);
        }


        /// <summary>
        /// the function find and display station information according to user id input
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Station DisplayStation(int id)
        {
            if (!dal.StationsList().Any(x => x.ID == id))
                throw new IBL.BO.Exceptions.IDException("Station ID not found", id);    //else client with id exist in dal

            IDAL.DO.Station dalStation = dal.StationsList().First(x => x.ID == id);

            Station station = new Station();                             //create new object and getting and assign the station info from datasource
            station.ChargingDronesList = new List<ChargingDrone>();        //new List for attributre of station

            station.ID = dalStation.ID; 
            station.Name = dalStation.Name;
            Location location = new Location();
            location.Latitude = dalStation.Latitude;
            location.Longitude = dalStation.Longitude;
            station.StationLocation = location;

            foreach (var item in dal.droneChargesList())    //finding charging drone in datasource 
            {
                if(dalStation.ID == item.StationId)
                {
                    ChargingDrone chargingDrone = new ChargingDrone();
                    chargingDrone.ID = item.DroneId;
                    DroneToList droneBat = DroneList.Find(drone => drone.ID == item.DroneId);
                    chargingDrone.Battery = droneBat.Battery;

                    station.ChargingDronesList.Add(chargingDrone);
                }
            }

            station.AvailableChargeSlots = dalStation.ChargeSlots;
            return station;
        }

        /// <summary>
        /// The function Display list of all station information
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StationToList> DisplayStationList()
        {
            List<StationToList> stations = new List<StationToList>();       //creating new list to return after assign

            foreach (var dalStation in dal.StationsList())
            {
                StationToList stationToList = new StationToList();  //new station object for list
                stationToList.ID = dalStation.ID;
                stationToList.Name = dalStation.Name;
                stationToList.AvailableChargingSlots = dalStation.ChargeSlots;

                List<IDAL.DO.DroneCharge> dronesInCharges = dal.droneChargesList().ToList().FindAll(d => d.StationId == dalStation.ID); //list of all charging drone in that current station
                stationToList.BusyChargingSlots = dronesInCharges.Count();                                                              //number of drone charging equal number of taken/busy chargeSlots

                stations.Add(stationToList);
            }

            return stations;
        }

        /// <summary>
        /// function that return list of station with available chargeSlot
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StationToList> DisplayStationListWitAvailableChargingSlots()
        {
            IEnumerable<StationToList> StationWithChargingSlots = DisplayStationList(); // using displaystationlist func to get the list of station

            StationWithChargingSlots = StationWithChargingSlots.Where(s => s.AvailableChargingSlots > 0); //  finding in that list all stations with Available chargeSlot 
            return StationWithChargingSlots;
        }


    }
}
