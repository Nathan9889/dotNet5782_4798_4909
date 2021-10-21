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

        static Random rand = new Random();
        public static double GetRandCoordinate(double num)
        {
            double res = num + rand.NextDouble() / 10;
            return res;
        }

        internal class Config
        {
            internal static int IndexDrone = 0;
            internal static int IndexStations = 0;
            internal static int IndexClients = 0;
            internal static int IndexPackage = 0;


            internal static int PackageId = 0;

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
                    Name = $"Client {Config.IndexClients}",
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
                    ID = Config.IndexDrone++,
                    Model = $"Drone {i}" ,
                    MaxWeight = (WeightCategories)rand.Next(3),
                    Status = (DroneStatus)rand.Next(3),
                    Battery = rand.Next(0,101)
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

        public static void AddClient()
        {
            Console.WriteLine("Enter Client Data\n");
            int clientId = Convert.ToInt32(Console.ReadLine());
            string clientName= Console.ReadLine();
            string clientPhone = Console.ReadLine();
            double clientLatitude = Convert.ToInt32(Console.ReadLine());
            double clientLongitude = Convert.ToInt32(Console.ReadLine());

            DataSource.ClientList.Add(
                new Client()
                {
                    ID = clientId,
                    Name = clientName,
                    Phone = clientPhone,
                    Latitude = clientLatitude,
                    Longitude = clientLongitude

                }                                   );
        }

        public static void AddPackage(Package package)
        {
            DataSource.PackageList.Add(package);
        }

        public void packageToDrone(Package package)
        {
            int idDrone = 0;
            foreach (var item in DataSource.DroneList)
            {
                if (item.Status == DroneStatus.Available)
                {
                    idDrone = item.ID;
                    Drone temp = item;
                    temp.Status = DroneStatus.Shipping;
                    DataSource.DroneList.Add(temp);
                    DataSource.DroneList.Remove(item);
                    break;
                }
            }
            if (idDrone == 0) throw new Exception("There are no drones available.");
            Package packageTemp = package;
            packageTemp.DroneId = idDrone;
            packageTemp.Scheduled = DateTime.Now;
            DataSource.PackageList.Add(packageTemp);
            DataSource.PackageList.Remove(package);
        }


    }




}