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
        //void AddClient(Client client);
        //int AddPackage(Package package);

        ////updating

        void UpdateDroneName(Drone drone);
        //void UpdateStation(Station station);
        //void UpdateClient(Client client);
        //void ChargeDrone(/*Drone drone);*/);
        //void FinishCharging(/*DroneCharge droneCharge*/);
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
