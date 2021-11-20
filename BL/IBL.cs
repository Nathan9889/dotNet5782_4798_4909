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
        //-----------ADD Functions------------
        public void AddDrone(Drone drone, int stationNumToCharge);
        public void AddStation(Station station);
        public int AddPackage(Package package);
        public void AddClient(Client client);



        //-----------Update Functions------------//
        public void UpdateDroneName(int id, string name);
        public void ChargeDrone(int id);
        public void FinishCharging(int droneID,Double minutesCharging);
        public void UpdateStation(int id, string name, int numCharge);
        public void packageToDrone(int droneID);
        void PickedUpByDrone(int droneID);
        void DeliveredToClient(int droneID);
        public void UpdateClient(int id, string name, string phone);



        //-----------Display Item------------//
        public Drone DisplayDrone(int id);
        public Station DisplayStation(int id);
        public Package DisplayPackage(int packageID);
       public Client DisplayClient(int id);



        //----------Display Lists------------//
        public IEnumerable<DroneToList> DisplayDroneList();
        public IEnumerable<StationToList> DisplayStationList();
        public IEnumerable<StationToList> DisplayStationListWitAvailableChargingSlots();
        public IEnumerable<PackageToList> DisplayPackageList();
        public IEnumerable<PackageToList> DisplayPackageListWithoutDrone();
        public IEnumerable<ClientToList> DisplayClientList();






        //Exit










    }

}
