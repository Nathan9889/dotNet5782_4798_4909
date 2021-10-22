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
        //static internal Drone[] Drones = new Drone[10];
        //static internal Station[] Stations = new Station[5];
        //static internal Client[] Clients = new Client[100];
        //static internal Package[] Packages = new Package[1000];


        internal static List<Client> ClientList = new List<Client>();
        internal static List<Drone> DroneList = new List<Drone>();
        internal static List<Station> StationList = new List<Station>();
        internal static List<Package> PackageList = new List<Package>();
        internal static List<DroneCharge> droneCharge = new List<DroneCharge>();

        static Random rand = new Random();
        public static double GetRandCoordinate(double num)
        {
            double res = num + rand.NextDouble() / 10;
            return res;
        }

        internal class Config
        {
            //internal static int IndexDrone = 0;
            //internal static int IndexStations = 0;
            //internal static int IndexClients = 0;
            //internal static int IndexPackage = 0;


            internal static int PackageId = 1000;

        }
        static void InitializeClient()
        {
            //ClientList = new List<Client>
            //{

            //    new Client
            //    {
            //    ID = rand.Next(100000, 1000000),
            //    Name = $"Client {Config.IndexClients}",
            //    Phone = $"0{rand.Next(50,58)} - {rand.Next(1000000,9999999)}",
            //    Latitude = GetRandCoordinate(29.5),
            //    Longitude = GetRandCoordinate(30.5)
            //    },

            //     new Client
            //    {
            //    ID = rand.Next(100000, 1000000),
            //    Name = $"Client {Config.IndexClients}",
            //    Phone = $"0{rand.Next(50,58)} - {rand.Next(1000000,9999999)}",
            //    Latitude = GetRandCoordinate(25.2),
            //    Longitude = GetRandCoordinate(28.3)
            //     },

            //      new Client
            //    {
            //      ID = rand.Next(100000, 1000000),
            //    Name = $"Client {Config.IndexClients}",
            //    Phone = $"0{rand.Next(50,58)} - {rand.Next(1000000,9999999)}",
            //    Latitude = GetRandCoordinate(22.3),
            //    Longitude = GetRandCoordinate(36.5)
            //    },

            //       new Client
            //    {
            //        ID = rand.Next(100000, 1000000),
            //        Name = $"Client {Config.IndexClients}",
            //        Phone = $"0{rand.Next(50,58)} - {rand.Next(1000000,9999999)}",
            //        Latitude = GetRandCoordinate(20.5),
            //        Longitude = GetRandCoordinate(23.2)
            //    },

            //        new Client
            //    {
            //         ID = rand.Next(100000, 1000000),
            //        Name = $"Client {Config.IndexClients}",
            //        Phone = $"0{rand.Next(50,58)} - {rand.Next(1000000,9999999)}",
            //        Latitude = GetRandCoordinate(23.6),
            //        Longitude = GetRandCoordinate(25.6)
            //    },

            //         new Client
            //    {
            //        ID = rand.Next(100000, 1000000),
            //        Name = $"Client {Config.IndexClients}",
            //        Phone = $"0{rand.Next(50,58)} - {rand.Next(1000000,9999999)}",
            //        Latitude = GetRandCoordinate(28.6),
            //        Longitude = GetRandCoordinate(27.8)
            //    },

            //          new Client
            //    {
            //     ID = rand.Next(100000, 1000000),
            //    Name = $"Client {Config.IndexClients}",
            //    Phone = $"0{rand.Next(50,58)} - {rand.Next(1000000,9999999)}",
            //    Latitude = GetRandCoordinate(23.6),
            //    Longitude = GetRandCoordinate(25.4)
            //    },

            //    new Client
            //    {
            //    ID = rand.Next(100000, 1000000),
            //    Name = $"Client {Config.IndexClients}",
            //    Phone = $"0{rand.Next(50,58)} - {rand.Next(1000000,9999999)}",
            //    Latitude = GetRandCoordinate(26.4),
            //    Longitude = GetRandCoordinate(29.5)
            //    },

            //    new Client
            //    {
            //         ID = rand.Next(100000, 1000000),
            //        Name = $"Client {Config.IndexClients}",
            //        Phone = $"0{rand.Next(50,58)} - {rand.Next(1000000,9999999)}",
            //        Latitude = GetRandCoordinate(30.2),
            //        Longitude = GetRandCoordinate(22.5)
            //    }





            //};

            ClientList = new List<Client>();
            for (int i = 0; i < 10; i++)
            {
                ClientList.Add(new Client()
                {
                    ID = rand.Next(100000, 1000000),
                    Name = $"Client {i}",
                    Phone = $"0{rand.Next(50, 58)} - {rand.Next(1000000, 9999999)}",
                    Latitude = GetRandCoordinate(29.5),
                    Longitude = GetRandCoordinate(30.5)

                });

            }

        }

        static void InitializeStation()
        {

            StationList = new List<Station>
            {
                new Station
                {
                    ID = rand.Next(10, 99),
                    Name = "Malha",
                    ChargeSlots = rand.Next(10),
                    Latitude = 29.12345,
                    Longitude = -32.654
                },

                new Station
                {
                    ID = rand.Next(10, 99),
                    Name = "Central",
                    ChargeSlots = rand.Next(10),
                    Latitude = 29.12345,
                    Longitude = -32.654
                },

              };
        }


        static void InitializeDrone()
        {

            //DroneList = new List<Drone>
            //{
            //    new Drone
            //    {
            //        ID = Config.IndexDrone++,
            //        Model = "Mavic",
            //        MaxWeight = (WeightCategories)rand.Next(3),
            //        Status = (DroneStatus)rand.Next(3),
            //        Battery = rand.Next(0,10)
            //    },

            //    new Drone
            //    {
            //        ID = Config.IndexDrone++,
            //        Model = "Mavic",
            //        MaxWeight = (WeightCategories)rand.Next(3),
            //        Status = (DroneStatus)rand.Next(3),
            //        Battery = rand.Next(0, 10)

            //    },


            //    new Drone
            //    {
            //        ID = Config.IndexDrone++,
            //        Model = "Mavic",
            //        MaxWeight = (WeightCategories)rand.Next(3),
            //        Status = (DroneStatus)rand.Next(3),
            //        Battery = rand.Next(0, 10)

            //    },

            //    new Drone
            //    {
            //        ID = Config.IndexDrone++,
            //        Model = "Mavic",
            //        MaxWeight = (WeightCategories)rand.Next(3),
            //        Status = (DroneStatus)rand.Next(3),
            //        Battery = rand.Next(0, 10)
            //    },

            //    new Drone
            //    {
            //        ID = Config.IndexDrone++,
            //        Model = "Mavic",
            //        MaxWeight = (WeightCategories)rand.Next(3),
            //        Status = (DroneStatus)rand.Next(3),
            //        Battery = rand.Next(0, 10)
            //    },

            //};

            DroneList = new List<Drone>();
            for (int i = 0; i < 10; i++)
            {
                DroneList.Add(new Drone()
                {
                    ID = rand.Next(1000000,10000000),
                    Model = $"Drone {i}" ,
                    MaxWeight = (WeightCategories)rand.Next(3),
                    Status = (DroneStatus)rand.Next(3),
                    Battery = rand.Next(0,101)
                });

            }

        }

        public static void InitializePackage()
        {
            for (int i = 0; i < 20; i++)
            {
                PackageList.Add(new Package()
                {
                    ID = Config.PackageId++,
                    SenderId = rand.Next(2000, 3000),
                    TargetId = rand.Next(3001, 4000),
                    Weight = (WeightCategories)rand.Next(3),
                    Priority = (Priorities)rand.Next(3),
                    DroneId = 0,

                });
            }
        }


        public static void Initialize()
        {
            InitializeClient();
            InitializeDrone();
            InitializeStation();


            ////initialize Drone, 
            //for (int i = 0; i < 5; i++)
            //{
            //    Drones[i].ID = rand.Next(100, 1000);
            //    Drones[i].Model = "Mavic";
            //    Drones[i].MaxWeight = WeightCategories.Medium ;
            //    Drones[i].Status = DroneStatus.Available;
            //    Drones[i].Battery = rand.Next(0, 100);
            //    Config.IndexStations++;
            //}




            //init Station

            //Stations[Config.IndexStations] = new Station()
            //{
            //    ID = rand.Next(10, 99),
            //    Name = "Base A",
            //    ChargeSlots = 3,
            //    Latitude = 29.12345,
            //    Longitude = -32.654
            //};
            //Config.IndexStations++;
            //Stations[Config.IndexStations] = new Station()
            //{
            //    ID = rand.Next(10, 99),
            //    Name = "Base A",
            //    ChargeSlots = 3,
            //    Latitude = 29.12345,
            //    Longitude = -32.654
            //};
            //Config.IndexStations++;





            //Init Drone 

            //Drones[Config.IndexDrone] = new Drone()
            //{
            //    ID = Config.IndexDrone,
            //    Model = "Mavic",
            //    MaxWeight = WeightCategories.Light,
            //    Status = DroneStatus.Available,
            //    Battery = rand.Next(0, 100)

            //};
            //Config.IndexDrone++;

            //Drones[Config.IndexDrone] = new Drone()
            //{
            //    ID = Config.IndexDrone,
            //    Model = "Mavic",
            //    MaxWeight = WeightCategories.Heavy,
            //    Status = DroneStatus.Maintenance,
            //    Battery = rand.Next(0, 100)

            //};
            //Config.IndexDrone++;

            //Drones[Config.IndexDrone] = new Drone()
            //{
            //    ID = Config.IndexDrone,
            //    Model = "Mavic",
            //    MaxWeight = WeightCategories.Medium,
            //    Status = DroneStatus.Shipping,
            //    Battery = rand.Next(0, 100)

            //};
            //Config.IndexDrone++;


            //Drones[Config.IndexDrone] = new Drone()
            //{
            //    ID = Config.IndexDrone,
            //    Model = "Mavic",
            //    MaxWeight = WeightCategories.Light,
            //    Status = DroneStatus.Maintenance,
            //    Battery = rand.Next(0, 100)

            //};
            //Config.IndexDrone++;


            //Drones[Config.IndexDrone] = new Drone()
            //{
            //    ID = Config.IndexDrone,
            //    Model = "Mavic",
            //    MaxWeight = WeightCategories.Medium,
            //    Status = DroneStatus.Available,
            //    Battery = rand.Next(0, 100)

            //};
            //Config.IndexDrone++;




            //init client


            //Clients[Config.IndexClients] = new Client()
            //{

            //    ID = rand.Next(1000, 9999),
            //    Name = "Jacob",
            //    Phone = "052-6137053",
            //    Latitude = 12.123,
            //    Longitude = -6.654
            //};
            //Config.IndexClients++;

            //Clients[Config.IndexClients] = new Client()
            //{

            //    ID = rand.Next(1000, 9999),
            //    Name = "Jhon",
            //    Phone = "052-5647578",
            //    Latitude = 23.626,
            //    Longitude = -7.351
            //};
            //Config.IndexClients++;

            //Clients[Config.IndexClients] = new Client()
            //{

            //    ID = rand.Next(1000, 9999),
            //    Name = "Nathan",
            //    Phone = "052-6454521",
            //    Latitude = 11.230211,
            //    Longitude = 15.326545
            //};
            //Config.IndexClients++;

            //Clients[Config.IndexClients] = new Client()
            //{

            //    ID = rand.Next(1000, 9999),
            //    Name = "Haim",
            //    Phone = "052-9876545",
            //    Latitude = -12.546532,
            //    Longitude = 25.321654
            //};
            //Config.IndexClients++;

            //Clients[Config.IndexClients] = new Client()
            //{

            //    ID = rand.Next(1000, 9999),
            //    Name = "Yossef",
            //    Phone = "052-3215645",
            //    Latitude = 10.235645,
            //    Longitude = -9.2654321
            //};
            //Config.IndexClients++;

            //Clients[Config.IndexClients] = new Client()
            //{

            //    ID = rand.Next(1000, 9999),
            //    Name = "Uriel",
            //    Phone = "052-2645543",
            //    Latitude = 3.326554,
            //    Longitude = -10.354565
            //};
            //Config.IndexClients++;

            //Clients[Config.IndexClients] = new Client()
            //{

            //    ID = rand.Next(1000, 9999),
            //    Name = "Elhanan",
            //    Phone = "052-2365451",
            //    Latitude = 2.654987,
            //    Longitude = 26.326554
            //};
            //Config.IndexClients++;

            //Clients[Config.IndexClients] = new Client()
            //{

            //    ID = rand.Next(1000, 9999),
            //    Name = "Dana",
            //    Phone = "052-9889455",
            //    Latitude = 13.236545,
            //    Longitude = -2.654321
            //};
            //Config.IndexClients++;

            //Clients[Config.IndexClients] = new Client()
            //{

            //    ID = rand.Next(1000, 9999),
            //    Name = "Eliav",
            //    Phone = "052-1356846",
            //    Latitude = 13.564532,
            //    Longitude = 21.321654
            //};
            //Config.IndexClients++;

            //Clients[Config.IndexClients] = new Client()
            //{

            //    ID = rand.Next(1000, 9999),
            //    Name = "David",
            //    Phone = "052-9876556",
            //    Latitude = 36.3256412,
            //    Longitude = -15.321656
            //};
            //Config.IndexClients++;


            ////init Package

            //Packages[Config.IndexPackage] = new Package()
            //{
            //    ID = rand.Next(100, 999),

            //};

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


        public Package packageById(int id) // Search for a package by id
        {
            foreach (var item in DataSource.PackageList)
            {
                if (item.ID == id) return item;
            }
            throw new Exception("There is no package with such an id");
        }

        public Drone droneById(int id) // Search for a drone by id
        {
            foreach (var item in DataSource.DroneList)
            {
                if (item.ID == id) return item;
            }
            throw new Exception("There is no drone with such an id");
        }

        public Station stationById(int id) // Search for a station by id
        {
            foreach (var item in DataSource.StationList)
            {
                if (item.ID == id) return item;
            }
            throw new Exception("There is no station with such an id");
        }


        public void packageToDrone(Package package) // Link the package to the drone
        {
            int idDrone = 0;
            foreach (var item in DataSource.DroneList)
            {
                if (item.Status == DroneStatus.Available) // If there is a drone available
                {
                    idDrone = item.ID;
                    Drone temp = item; // Updates in temp of drone
                    temp.Status = DroneStatus.Shipping; // Updates in temp of drone
                    DataSource.DroneList.Add(temp); // Add temp to list and delete old
                    DataSource.DroneList.Remove(item);
                    break;
                }
            }
            if (idDrone == 0) throw new Exception("There are no drones available.");
            Package packageTemp = package;
            packageTemp.DroneId = idDrone; //  Updates in temp of  package
            packageTemp.Scheduled = DateTime.Now;//  package
            DataSource.PackageList.Add(packageTemp); // Add temp to list and delete old
            DataSource.PackageList.Remove(package);
        }

        public void PickedUpByDrone(Package package)// Package collection by drone
        {
            Package packageTemp = package;
            packageTemp.PickedUp = DateTime.Now; // Updates in temp of drone
            DataSource.PackageList.Add(packageTemp); // Add temp to list and delete old
            DataSource.PackageList.Remove(package);
        }

        public void DeliveredToClient(Package package) // The package was delivered to the client
        {

            Drone drone = droneById(package.DroneId);
            Drone droneTemp = drone;
            droneTemp.Status = DroneStatus.Available; // Updates in temp of drone
            DataSource.DroneList.Add(droneTemp); // Add temp to list and delete old
            DataSource.DroneList.Remove(drone);

            Package packageTemp = package;
            packageTemp.Delivered = DateTime.Now; // Updates in temp of  package
            DataSource.PackageList.Add(packageTemp); //Add temp to list and delete old
            DataSource.PackageList.Remove(package);

        }

        public Station chargingStation() // The user has to select a charging station. And update the station. And reduce charging positions
        {

            return
        }

        public void DroneCharge(Drone drone)
        {
            Station station = chargingStation(); // The station that the user choose
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


        public void finishCharging(DroneCharge droneCharge) // Finish drone Chargeing, update drone status and update station
        {
            Drone drone = droneById(droneCharge.DroneId);
            Drone droneTemp = drone; //   Updates in temp of drone
            droneTemp.Status = DroneStatus.Available; //    Updates in temp of drone
            droneTemp.Battery = 100; //    Updates in temp of drone
            DataSource.DroneList.Add(droneTemp); // Add temp to list and delete old
            DataSource.DroneList.Remove(drone);


            Station station = stationById(droneCharge.StationId); // The station that Charged the drone
            Station stationTemp = station;
            stationTemp.ChargeSlots--; // Updates in temp of station 
            DataSource.StationList.Add(stationTemp); // Add temp to list and delete old
            DataSource.StationList.Remove(station);

            DataSource.droneCharge.Remove(droneCharge); // Deleting the instance from the list
        }






    }
}