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
        //-----------ADD Functions------------//
        void AddStation(Station station);
        void AddDrone(Drone drone, int stationNumToCharge);
        void AddClient(Client client);
        void AddPackage(Package package);

        //-----------Update Functions------------//
        void UpdateDroneName(int id, string name);
        void UpdateStation(int id, string name, int numCharge);
        void UpdateClient(int id, string name, string phone);
        void ChargeDrone(int id);
        void FinishCharging(int DroneID, int minutesCharging);
        //void packageToDrone(/*Package package, int DroneID*/);
        //void PickedUpByDrone(/*Package package*/);
        //void DeliveredToClient(/*Package package*/);
        IEnumerable<IDAL.DO.Client> RecivedCustomerList();

        //-----------Display Item------------//
        Station DisplayStation(int id);
        Drone DroneItem(int id);
        Client DisplayClient(int id);

        //-----------Display Functions------------//

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
