using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    internal partial class BL : BlApi.IBL        //Partial BL class that contains Stations Functions
    {

        /// <summary>
        /// The function get a object station from user input and adds it to the station list in Datasource 
        /// </summary>
        /// <param name="station"> Station object from ConsoleUi </param>
        public void AddStation(Station station)
        {
            if (station.ID < 0)                                                                         // Id input exceptions
                throw new BO.Exceptions.NegativeException("Station ID can not be negative", station.ID);
            if (station.AvailableChargeSlots < 0)
                throw new BO.Exceptions.NegativeException("Charges slot cannot be negative", station.AvailableChargeSlots);
            if (((station.StationLocation.Latitude < 31.73) || (station.StationLocation.Latitude > 31.83)) ||
               ((station.StationLocation.Longitude <= 35.16) || (station.StationLocation.Longitude >= 35.26)))      //location exception 
            {
                throw new Exceptions.LocationOutOfRange("Station Location entered is out of shipping range", station.ID);
            }

            DO.Station dalStation = new DO.Station();   //new datasource station then assigning

            dalStation.ID = station.ID;
            dalStation.Name = station.Name;
            if (!((station.StationLocation.Latitude >= 31.73) && (station.StationLocation.Latitude <= 31.83) ||
                (station.StationLocation.Longitude >= 35.16) && (station.StationLocation.Longitude <= 35.26)))    //if station info inputed is out of shipping range
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
            catch (DO.Exceptions.IDException ex)
            {
                throw new Exceptions.IDException("Station with this ID already exists", ex, station.ID);
            }
        }

        /// <summary>
        /// The function update the name of the station that matches the id user inputed
        /// </summary>
        /// <param name="id"> id to find which station user wants to change the name</param>
        /// <param name="name"> new name to give to the station</param>
        public void UpdateStationName(int id, string name)
        {
            DO.Station dalStation;
            if (!dal.StationsList().Any(x => x.ID == id))  // if station doesn't exist
                throw new BO.Exceptions.IDException("Station ID not found", id);
            dalStation = dal.StationById(id);

            DO.Station stationTemp = dalStation;
            stationTemp.Name = name;

            dal.DeleteStation(dalStation.ID);  //switching station with the new name
            dal.AddStation(stationTemp);
        }

        /// <summary>
        /// The function update the number of charge slot of the station that matches the id user inputed
        /// </summary>
        /// <param name="id">id to find which station user wants to change the name </param>
        /// <param name="numCharge">new number of charge slot to give to the given station </param>
        public void UpdateStationNumCharge(int id, int numCharge)
        {
            DO.Station dalStation;
            if (!dal.StationsList().Any(x => x.ID == id))
                throw new BO.Exceptions.IDException("Station ID not found", id);

            dalStation = dal.StationById(id);  //getting station info to give to new one then exchange between
            DO.Station stationTemp = dalStation;

            int minSlots = 0;
            foreach (var item in dal.droneChargesList())  //count if num inputed is greater than num of drones charging in that staion
            {
                if (item.StationId == id) minSlots++;
            }
            if (numCharge < minSlots)
                throw new BO.Exceptions.StationException("The number of new slots can not be less than the number of Drones in charging.", id);

            stationTemp.ChargeSlots = numCharge;  //update

            dal.DeleteStation(dalStation.ID);   //switch
            dal.AddStation(stationTemp);
        }


        /// <summary>
        /// the function find the station according to id, create new BL station to return it to display the station information 
        /// </summary>
        /// <param name="id"> id inputed in ConsoleUi to find which station to display </param>
        /// <returns> returns StationBl object </returns>
        public Station DisplayStation(int id)
        {
            if (!dal.StationsList().Any(x => x.ID == id))
                throw new BO.Exceptions.IDException("Station ID not found", id);    //else client with id exist in dal

            DO.Station dalStation = dal.StationsList().First(x => x.ID == id);

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
                if (dalStation.ID == item.StationId)
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
        /// <returns> Returns List of stations </returns>
        public IEnumerable<StationToList> DisplayStationList()
        {
            List<StationToList> stations = new List<StationToList>();       //creating new list to return after assign

            foreach (var dalStation in dal.StationsList())
            {
                StationToList stationToList = new StationToList();  //new station object for list
                stationToList.ID = dalStation.ID;
                stationToList.Name = dalStation.Name;
                stationToList.AvailableChargingSlots = dalStation.ChargeSlots;

                List<DO.DroneCharge> dronesInCharges = dal.DroneChargeFilter(d => d.StationId == dalStation.ID).ToList(); //list of all charging drone in that current station
                stationToList.BusyChargingSlots = dronesInCharges.Count();                                                              //number of drone charging equal number of taken/busy chargeSlots

                stations.Add(stationToList);
            }

            return stations;
        }

        /// <summary>
        /// function that return list of station with available chargeSlot
        /// </summary>
        /// <returns> Returns List of stations </returns>
        public IEnumerable<StationToList> DisplayStationListWitAvailableChargingSlots()
        {
            IEnumerable<StationToList> StationWithChargingSlots = DisplayStationList(); // using displaystationlist func to get the list of station

            StationWithChargingSlots = StationWithChargingSlots.Where(s => s.AvailableChargingSlots > 0); //  finding in that list all stations with Available chargeSlot 
            return StationWithChargingSlots;
        }

        //public IEnumerable<IGrouping<bool,StationToList>> GroupStationByExistingSlots()
        //{
        //   var list = DisplayStationList().GroupBy(s=> s.AvailableChargingSlots > 0);
        //   return list;
        //}

        public IEnumerable<IGrouping<int, StationToList>> GroupStationByNumSlots()
        {
            return DisplayStationList().GroupBy(s => s.AvailableChargingSlots);
        }

        public void DeleteStation(int ID)
        {
            if (!DisplayStationList().Any(p => p.ID == ID)) throw new Exceptions.CantDelete(ID, "ID To Delete Not Found");
            try
            {
                dal.DeleteStation(ID);
            }
            catch (Exception ex)
            {

                throw new Exceptions.IDException("ID To Delete Not Found", ex, ID);
            }

        }

    }
}
