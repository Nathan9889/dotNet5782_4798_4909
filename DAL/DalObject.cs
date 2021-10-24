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
        internal static List<Client> ClientList = new List<Client>();//Creating List of Clients
        internal static List<Drone> DroneList = new List<Drone>(); // Creating List of Drone
        internal static List<Station> StationList = new List<Station>(); // Creating List of Station
        internal static List<Package> PackageList = new List<Package>(); // Creating List of Package
        internal static List<DroneCharge> droneCharge = new List<DroneCharge>(); // Creating List of droneCharge

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

        /// <summary>
        /// Initialize 10 clients
        /// </summary>
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


        /// <summary>
        /// Initialize 3 stations
        /// </summary>
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


        /// <summary>
        /// Initialize 10 drones
        /// </summary>
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


        /// <summary>
        /// Initialize 10 Packages
        /// </summary>
        public static void InitializePackage()
        {
            for (int i = 0; i < 10; i++)
            {
                PackageList.Add(new Package()
                {
                    ID = Config.PackageId++,
                    SenderId = ClientList[rand.Next(0,10)].ID,
                    TargetId = ClientList[rand.Next(0, 10)].ID,
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


        /// <summary>
        /// The function receives an object Station and adds it to the list
        /// </summary>
        /// <param name="station"></param>
        public static void AddStation(Station station)
        {
            DataSource.StationList.Add(station);
        }


        /// <summary>
        /// The function gets a drone object and adds it to the list
        /// </summary>
        /// <param name="drone"></param>
        public static void AddDrone(Drone drone)
        {
            DataSource.DroneList.Add(drone);
        }


        /// <summary>
        /// The function accepts a client-type object and adds it to the list of clients
        /// </summary>
        /// <param name="client"></param>
        public static void AddClient(Client client)
        {
            DataSource.ClientList.Add(client);
        }


        /// <summary>
        /// The function receives a package type object and adds it to the list of packages
        /// </summary>
        /// <param name="package"></param>
        public static void AddPackage(Package package)
        {
            DataSource.PackageList.Add(package);
        }


        /// <summary>
        /// The function accepts a client's id and returns the object that is its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Client ClientById(int id) 
        {
            foreach (var item in DataSource.ClientList)
            {
                if (item.ID == id) return item;
            }
            Client null1 = new Client { };
            return null1;
        }

        
        /// <summary>
        /// The function receives a packet ID and returns the object whose ID it is
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Package PackageById(int id) 
        {
            foreach (var item in DataSource.PackageList)
            {
                if (item.ID == id) return item;
            }
            Package null1 = new Package { };
            return null1;
        }


        /// <summary>
        /// The function receives a drone ID and returns the object that is its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Drone DroneById(int id) 
        {
            foreach (var item in DataSource.DroneList)
            {
                if (item.ID == id) return item;
            }
            Drone null1 = new Drone { };
            return null1;
        }


        /// <summary>
        /// The function receives a station ID and returns the object whose ID it is
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Station StationById(int id) 
        {
            foreach (var item in DataSource.StationList)
            {
                if (item.ID == id) return item;
            }
            Station null1 = new Station { };
            return null1;
        }


        /// <summary>
        /// The function gets a DroneCharge ID and returns the object that is its DroneID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DroneCharge DroneChargeByIdDrone(int id) 
        {
            foreach (var item in DataSource.droneCharge)
            {
                if (item.DroneId == id) return item;
            }
            DroneCharge null1 = new DroneCharge { };
            return null1;
        }


        /// <summary>
        /// The function receives a Drone object and a Package object and assigns the packet to the drone
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

        /// <summary>
        /// The function receives a Package object and updates the pick-up time by the drone
        /// </summary>
        /// <param name="package"></param>
        public static void PickedUpByDrone(Package package)// Package collection by drone
        {
            Package packageTemp = package;
            packageTemp.PickedUp = DateTime.Now; // Updates in temp of drone
            DataSource.PackageList.Add(packageTemp); // Add temp to list and delete old
            DataSource.PackageList.Remove(package);
        }


        /// <summary>
        /// The function receives a package object and updates that the package has been delivered to the customer.
        ///Updates the delivery time and the drone is available
        /// </summary>
        /// <param name="package"></param>
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


        /// <summary>
        /// The function receives a station ID, updates on one occupied charging station. And returns the object
        /// </summary>
        /// <param name="stationID"></param>
        /// <returns></returns>
        public static Station ChargingStation(int stationID) // The function handles the station
        {
            Station station = StationById(stationID);
            Station stationTemp = station;
            stationTemp.ChargeSlots--;
            DataSource.StationList.Add(stationTemp);
            DataSource.StationList.Remove(station);
            return stationTemp;
        }


        /// <summary>
        /// The function receives a station ID, sends it to the ChargingStation function and receives the requested station.
        ///The function receives a drone, updates it that it is charging and creates a new DroneCharg instance with the details of the charging station and the loaded drone
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="stationID"></param>
        public static void DroneCharge(Drone drone,int stationID)
        {
            Station station = ChargingStation(stationID); // The station that the user choose
            Drone droneTemp = drone;
            droneTemp.Status = DroneStatus.Maintenance; // Updates in temp of drone
            DroneCharge droneCharg = new DroneCharge() // Initialization of a new instance for DroneCharge
            {
                DroneId = droneTemp.ID,
                StationId = station.ID
            };
            DataSource.droneCharge.Add(droneCharg); // Add the instance to the list

            DataSource.DroneList.Add(droneTemp); // Add temp to list and delete old
            DataSource.DroneList.Remove(drone);
        }


        /// <summary>
        /// The function receives a DroneCharge instance and updates the station and drone upon completion of charging
        /// </summary>
        /// <param name="droneCharge"></param>
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


        /// <summary>
        /// The function returns the list of stations
        /// </summary>
        /// <returns></returns>
        public static List<Station> StationsList()
        {
            return DataSource.StationList;
        }


        /// <summary>
        /// The function returns the list of drones
        /// </summary>
        /// <returns></returns>
        public static List<Drone> DroneList()
        {
            return DataSource.DroneList;
        }


        /// <summary>
        /// The function returns the list of clients
        /// </summary>
        /// <returns></returns>
        public static List<Client> ClientsList()
        {
            return DataSource.ClientList;
        }


        /// <summary>
        /// The function returns the list of packages
        /// </summary>
        /// <returns></returns>
        public static List<Package> PackageList()
        {
            return DataSource.PackageList;
        }


        /// <summary>
        /// The function creates and returns a list of packages that have not yet been assigned to the glider
        /// </summary>
        /// <returns></returns>
        public static List<Package> PackageWithoutDrone() 
        {
            List<Package> packagesWithoutDrone = new List<Package>();
            foreach (var item in DataSource.PackageList)
            {
                if (item.DroneId == 0) packagesWithoutDrone.Add(item);
            }
            return packagesWithoutDrone;
        }


        /// <summary>
        /// The function creates and returns a list of stations with available charging slots
        /// </summary>
        /// <returns></returns>
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