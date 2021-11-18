using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public interface IBL
    {
        ////adding
        void AddStation(Station station);
        void AddDrone(Drone drone, int stationNumToCharge);
        void AddClient(Client client);
        void AddPackage(Package package);

        ////updating

        void UpdateDroneName(int id, string name);
        void UpdateStation(int id, string name, int numCharge);
        void UpdateClient(int id, string name, string phone);
        void ChargeDrone(int ID);
        void FinishCharging(int DroneID, int minutesCharging);
        //void packageToDrone(/*Package package, int DroneID*/);
        //void PickedUpByDrone(/*Package package*/);
        //void DeliveredToClient(/*Package package*/);


        ////Diplays

        ////
        ////
        ////
        ////
        //IEnumerable<Station> StationsList();
        //IEnumerable<Drone> DroneList();
        //IEnumerable<Client> ClientsList();
        //IEnumerable<Package> PackageList();
        //IEnumerable<Package> PackageWithoutDrone();
        //IEnumerable<Station> StationWithCharging();



        ////Exit










    }

}
