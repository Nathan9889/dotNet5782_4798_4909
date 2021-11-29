using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using DalObject;

namespace IDAL
{
    public interface IDAL
    {
        /// <summary>
        /// The function receives an object Station and adds it to the list
        /// </summary>
        /// <param name="station"></param>
        void AddStation(Station station);

        /// <summary>
        /// The function gets a drone object and adds it to the list
        /// </summary>
        /// <param name="drone"></param>
        void AddDrone(Drone drone);

        /// <summary>
        /// The function accepts a client-type object and adds it to the list of clients
        /// </summary>
        /// <param name="client"></param>
        void AddClient(Client client);

        /// <summary>
        /// The function receives a package type object and adds it to the list of packages
        /// </summary>
        /// <param name="package"></param>
        int AddPackage(Package package);

        /// <summary>
        /// The function accepts a client's id and returns the object that is its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Client ClientById(int id);

        /// <summary>
        /// The function receives a packet ID and returns the object whose ID it is
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Package PackageById(int id);

        /// <summary>
        /// The function receives a drone ID and returns the object that is its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Drone DroneById(int id);

        /// <summary>
        /// The function receives a station ID and returns the object whose ID it is
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Station StationById(int id);

        /// <summary>
        /// The function gets a DroneCharge ID and returns the object that is its DroneID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DroneCharge DroneChargeByIdDrone(int id);

        /// <summary>
        /// The function receives a Drone object and a Package object and assigns the packet to the drone
        /// </summary>
        /// <param name="package"></param>
        /// <param name="DroneID"></param>
        void packageToDrone(Package package, int DroneID);

        /// <summary>
        /// The function receives a Package object and updates the pick-up time by the drone
        /// </summary>
        /// <param name="package"></param>
        void PickedUpByDrone(Package package);

        /// <summary>
        /// The function receives a package object and updates that the package has been delivered to the customer.
        ///Updates the delivery time and the drone is available
        /// </summary>
        /// <param name="package"></param>
        void DeliveredToClient(Package package);

        /// <summary>
        /// The function receives a station ID, updates on one occupied charging station. And returns the object
        /// </summary>
        /// <param name="stationID"></param>
        /// <returns></returns>
        Station ChargingStation(int stationID);

        /// <summary>
        /// The function receives a station ID, sends it to the ChargingStation function and receives the requested station.
        ///The function receives a drone, updates it that it is charging and creates a new DroneCharg instance with the details of the charging station and the loaded drone
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="stationID"></param>
        void DroneCharge(Drone drone, int stationID);

        /// <summary>
        /// The function receives a DroneCharge instance and updates the station and drone upon completion of charging
        /// </summary>
        /// <param name="droneCharge"></param>
        void FinishCharging(DroneCharge droneCharge);

        /// <summary>
        /// returns dronecharge list type
        /// </summary>
        /// <returns> chargedrone list </returns>
        IEnumerable<DroneCharge> droneChargesList();

        /// <summary>
        /// The function returns the list of stations
        /// </summary>
        /// <returns >station list</returns>
        IEnumerable<Station> StationsList();

        /// <summary>
        /// The function returns the list of drones
        /// </summary>
        /// <returns></returns>
        IEnumerable<Drone> DroneList();

        /// <summary>
        /// The function returns the list of clients
        /// </summary>
        /// <returns></returns>
        IEnumerable<Client> ClientsList();

        /// <summary>
        /// The function returns the list of packages
        /// </summary>
        /// <returns></returns>
        IEnumerable<Package> PackageList();

        /// <summary>
        /// The function creates and returns a list of packages that have not yet been assigned to the glider
        /// </summary>
        /// <returns></returns>
        IEnumerable<Package> PackageWithoutDrone();

        /// <summary>
        /// The function creates and returns a list of stations with available charging slots
        /// </summary>
        /// <returns></returns>
        IEnumerable<Station> StationWithCharging();

        /// <summary>
        /// remove item drone from the list of drones
        /// </summary>
        /// <param name="drone"></param>
        void DeleteDrone(Drone drone);

        /// <summary>
        /// remove item station from the list of station
        /// </summary>
        /// <param name="station"></param>
        void DeleteStation(Station station);

        /// <summary>
        /// remove item client from the list of client
        /// </summary>
        /// <param name="client"></param>
        void DeleteClient(Client client);

        /// <summary>
        /// remove item package from the list of package
        /// </summary>
        /// <param name="package"></param>
        void DeletePackage(Package package);

        /// <summary>
        /// remove item droneCharge from the list of droneCharge
        /// </summary>
        /// <param name="droneCharge"></param>
        void DeleteDroneCharge(DroneCharge droneCharge);
        double[] PowerConsumptionByDrone();


    }


}
