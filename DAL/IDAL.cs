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
        void AddStation(Station station);
        void AddDrone(Drone drone);
        void AddClient(Client client);
        int AddPackage(Package package);
        Client ClientById(int id);
        Package PackageById(int id);
        Drone DroneById(int id);
        Station StationById(int id);
        DroneCharge DroneChargeByIdDrone(int id);
        void packageToDrone(Package package, int DroneID);
        void PickedUpByDrone(Package package);
        void DeliveredToClient(Package package);
        Station ChargingStation(int stationID);
        void DroneCharge(Drone drone, int stationID);
        void FinishCharging(DroneCharge droneCharge);
        IEnumerable<Station> StationsList();
        IEnumerable<Drone> DroneList();
        IEnumerable<Client> ClientsList();
        IEnumerable<Package> PackageList();
        IEnumerable<Package> PackageWithoutDrone();
        IEnumerable<Station> StationWithCharging();
        IEnumerable<DroneCharge> droneChargesList();
        void DeleteDrone(Drone drone);
        void DeleteStation(Station station);
        void DeleteClient(Client client);
        void DeletePackage(Package package);
        void DeleteDroneCharge(DroneCharge droneCharge);
        double[] PowerConsumptionByDrone();


    }


}
