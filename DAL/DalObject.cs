using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;


namespace DalObject
{
    public class DataSource
    {
        internal static List<Client> ClientList = new List<Client>();    //Creating List of Clients
        internal static List<Drone> DroneList = new List<Drone>();      
        internal static List<Station> StationList = new List<Station>();
        internal static List<Package> PackageList = new List<Package>();
        internal static List<DroneCharge> droneCharge = new List<DroneCharge>();

        static Random rand = new Random();
        public static double GetRandCoordinate(double num) //return Coordinate
        {
            double res = num + rand.NextDouble() / 10;
            return Math.Round(res, 6);
        }

        internal class Config
        {
            internal static int PackageId = 1000;
        }

        public static void Initialize()
        {
            InitializeClient();
            InitializeStation();
            InitializeDrone();
            InitializePackage();
        }


        static void InitializeClient()
        {
            for (int i = 0; i < 10; i++)
            {
                ClientList.Add(new Client()
                {
                    ID = rand.Next(100000, 1000000),
                    Name = $"Client {i}",
                    Phone = $"0{rand.Next(51, 58)}{rand.Next(1000000, 9999999)}",
                    Latitude = GetRandCoordinate(31.73),
                    Longitude = GetRandCoordinate(35.16)
                }) ;
            }
        }
        static void InitializeStation()
        {

            StationList.Add(new Station()
            {
                ID = rand.Next(10, 99),
                Name = "Malcha Mall",
                ChargeSlots = rand.Next(10),
                Latitude = 31.7515163,
                Longitude = 35.1872451
            });

            StationList.Add(new Station()
            {
                ID = rand.Next(10, 99),
                Name = "Central Station",
                ChargeSlots = rand.Next(10),
                Latitude = 31.7888727,
                Longitude = 35.2031491
            });

            StationList.Add(new Station()
            {
                ID = rand.Next(10, 99),
                Name = "Mount Scopus",
                ChargeSlots = rand.Next(10),
                Latitude = 31.7930604,
                Longitude = 35.2449342
            });
        }
        static void InitializeDrone()
        {
            
            for (int i = 0; i < 10; i++)
            {
                DroneList.Add(new Drone()
                {
                    ID = rand.Next(1000000,10000000),
                    Model = $"{(DroneModel)rand.Next(4)}" ,
                    MaxWeight = (WeightCategories)rand.Next(3),
                    Status = (DroneStatus)rand.Next(3),
                    Battery = rand.Next(0,101)
                });
            }
        }
        public static void InitializePackage()
        {
            for (int i = 0; i < 10; i++)
            {
                PackageList.Add(new Package()
                {
                    ID = Config.PackageId++,
                    SenderId = rand.Next(20000, 30000),
                    TargetId = rand.Next(30000, 40000),
                    Weight = (WeightCategories)rand.Next(3),
                    Priority = (Priorities)rand.Next(3),
                    DroneId = 0,
                    Created = DateTime.Now
                }) ;
            }
        }
        
    }
    public class DalObject
    {

        public DalObject() { DataSource.Initialize(); }


        public static void AddStation(Station station)
        {
            DataSource.StationList.Add(station);
        }

        public static void AddDrone(Drone drone)
        {
            DataSource.DroneList.Add(drone);
        }

        public static void AddClient(Client client)
        {
            DataSource.ClientList.Add(client);
        }

        public static void AddPackage(Package package)
        {
            DataSource.PackageList.Add(package);
        }

        public static Client ClientById(int id) // Search for a client by id
        {
            foreach (var item in DataSource.ClientList)
            {
                if (item.ID == id) return item;
            }
            throw new Exception("There is no client with such an id");
        }

        public static Package PackageById(int id) // Search for a package by id
        {
            foreach (var item in DataSource.PackageList)
            {
                if (item.ID == id) return item;
            }
            throw new Exception("There is no package with such an id");
        }

        public static Drone DroneById(int id) // Search for a drone by id
        {
            foreach (var item in DataSource.DroneList)
            {
                if (item.ID == id) return item;
            }
            throw new Exception("There is no drone with such an id");
        }

        public static Station StationById(int id) // Search for a station by id
        {
            foreach (var item in DataSource.StationList)
            {
                if (item.ID == id) return item;
            }
            throw new Exception("There is no station with such an id");
        }

        public static DroneCharge DroneChargeByIdDrone(int id) // Search for a  DroneCharge by id of drone
        {
            foreach (var item in DataSource.droneCharge)
            {
                if (item.DroneId == id) return item;
            }
            throw new Exception("There is no  drone in charging with such an id");
        }

        public static void packageToDrone(Package package, Drone drone) // Link the package to the drone
        {
            Drone temp = drone;  // Updates in temp of drone
            temp.Status = DroneStatus.Shipping; // Updates in temp of drone
            DataSource.DroneList.Add(temp); // Add temp to list and delete old
            DataSource.DroneList.Remove(drone);


            Package packageTemp = package;
            packageTemp.DroneId = temp.ID; //  Updates in temp of  package
            packageTemp.Associated = DateTime.Now;//  package
            DataSource.PackageList.Add(packageTemp); // Add temp to list and delete old
            DataSource.PackageList.Remove(package);
        }

        public static void PickedUpByDrone(Package package)// Package collection by drone
        {
            Package packageTemp = package;
            packageTemp.PickedUp = DateTime.Now; // Updates in temp of drone
            DataSource.PackageList.Add(packageTemp); // Add temp to list and delete old
            DataSource.PackageList.Remove(package);
        }

        public static void DeliveredToClient(Package package) // The package was delivered to the client
        {

            Drone drone = DroneById(package.DroneId);
            Drone droneTemp = drone;
            droneTemp.Status = DroneStatus.Available; // Updates in temp of drone
            DataSource.DroneList.Add(droneTemp); // Add temp to list and delete old
            DataSource.DroneList.Remove(drone);

            Package packageTemp = package;
            packageTemp.Delivered = DateTime.Now; // Updates in temp of  package
            DataSource.PackageList.Add(packageTemp); //Add temp to list and delete old
            DataSource.PackageList.Remove(package);

        }

        public static Station ChargingStation(int stationID) // The function handles the station
        {
            Station station = StationById(stationID);
            Station stationTemp = station;
            stationTemp.ChargeSlots--;
            DataSource.StationList.Add(stationTemp);
            DataSource.StationList.Remove(station);
            return stationTemp;
        }

        public static void DroneCharge(Drone drone,int stationID)
        {
            Station station = ChargingStation(stationID); // The station that the user choose
            Station stationTemp = station;
            Drone droneTemp = drone;
            droneTemp.Status = DroneStatus.Maintenance; // Updates in temp of drone
            DroneCharge droneCharg = new DroneCharge() // Initialization of a new instance for DroneCharge
            {
                DroneId = droneTemp.ID,
                StationId = stationTemp.ID
            };
            DataSource.droneCharge.Add(droneCharg); // Add the instance to the list

            DataSource.DroneList.Add(droneTemp); // Add temp to list and delete old
            DataSource.DroneList.Remove(drone);

            stationTemp.ChargeSlots--;
            DataSource.StationList.Add(stationTemp);
            DataSource.StationList.Remove(station);
        }


        public static void FinishCharging(DroneCharge droneCharge) // Finish drone Chargeing, update drone status and update station
        {
            Drone drone = DroneById(droneCharge.DroneId);
            Drone droneTemp = drone; //   Updates in temp of drone
            droneTemp.Status = DroneStatus.Available; //    Updates in temp of drone
            droneTemp.Battery = 100; //    Updates in temp of drone
            DataSource.DroneList.Add(droneTemp); // Add temp to list and delete old
            DataSource.DroneList.Remove(drone);


            Station station = StationById(droneCharge.StationId); // The station that Charged the drone
            Station stationTemp = station;
            stationTemp.ChargeSlots--; // Updates in temp of station 
            DataSource.StationList.Add(stationTemp); // Add temp to list and delete old
            DataSource.StationList.Remove(station);

            DataSource.droneCharge.Remove(droneCharge); // Deleting the instance from the list
        }


        public static List<Station> StationsList()
        {
            return DataSource.StationList;
        }

        public static List<Drone> DroneList()
        {
            return DataSource.DroneList;
        }

        public static List<Client> ClientsList()
        {
            return DataSource.ClientList;
        }

        public static List<Package> PackageList()
        {
            return DataSource.PackageList;
        }

        public static List<Package> PackageWithoutDrone()
        {
            List<Package> packagesWithoutDrone = new List<Package>();
            foreach (var item in DataSource.PackageList)
            {
                if (item.DroneId == 0) packagesWithoutDrone.Add(item);
            }
            return packagesWithoutDrone;
        }

        public static List<Station> StationWithCharging()
        {
            List<Station> stationWithCharging = new List<Station>();
            foreach (var item in DataSource.StationList)
            {
                if (item.ChargeSlots > 0) stationWithCharging.Add(item);
            }
            return stationWithCharging;
        }
    }
}