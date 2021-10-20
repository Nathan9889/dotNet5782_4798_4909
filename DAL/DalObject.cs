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
        static internal Drone[] Drones = new Drone[10];
        static internal Station[] Stations = new Station[5];
        static internal Client[] Clients = new Client[100];
        static internal Package[] Packages = new Package[1000];

        public static Random rand = new Random();

        internal class Config
        {
            internal static int IndexDrone = 0;
            internal static int IndexStations = 0;
            internal static int IndexClients = 0;
            internal static int IndexPackage = 0;

        }

        
        public static void Initialize()
        {
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

            Stations[Config.IndexStations] = new Station()
            {
                ID = rand.Next(10, 99),
                Name = "Base A",
                ChargeSlots = 3,
                Latitude = 29.12345,
                Longitude = -32.654
            };
            Config.IndexStations++;
            Stations[Config.IndexStations] = new Station()
            {
                ID = rand.Next(10, 99),
                Name = "Base A",
                ChargeSlots = 3,
                Latitude = 29.12345,
                Longitude = -32.654
            };
            Config.IndexStations++;





            //Init Drone 

            Drones[Config.IndexDrone] = new Drone()
            {
                ID = Config.IndexDrone,
                Model = "Mavic",
                MaxWeight = WeightCategories.Light,
                Status = DroneStatus.Available,
                Battery = rand.Next(0,100)

            };
            Config.IndexDrone++;

            Drones[Config.IndexDrone] = new Drone()
            {
                ID = Config.IndexDrone,
                Model = "Mavic",
                MaxWeight = WeightCategories.Heavy,
                Status = DroneStatus.Maintenance,
                Battery = rand.Next(0, 100)

            };
            Config.IndexDrone++; 
            
            Drones[Config.IndexDrone] = new Drone()
            {
                ID = Config.IndexDrone,
                Model = "Mavic",
                MaxWeight = WeightCategories.Medium,
                Status = DroneStatus.Shipping,
                Battery = rand.Next(0, 100)

            };
            Config.IndexDrone++; 
            
            
            Drones[Config.IndexDrone] = new Drone()
            {
                ID = Config.IndexDrone,
                Model = "Mavic",
                MaxWeight = WeightCategories.Light,
                Status = DroneStatus.Maintenance,
                Battery = rand.Next(0, 100)

            };
            Config.IndexDrone++; 
            
            
            Drones[Config.IndexDrone] = new Drone()
            {
                ID = Config.IndexDrone,
                Model = "Mavic",
                MaxWeight = WeightCategories.Medium,
                Status = DroneStatus.Available,
                Battery = rand.Next(0, 100)

            };
            Config.IndexDrone++;







            //init client


            Clients[Config.IndexClients] = new Client()
            {

                ID = rand.Next(1000, 9999),
                Name = "Jacob",
                Phone = "052-6137053",
                Latitude = 12.123,
                Longitude = -6.654
            };
            Config.IndexClients++;

            Clients[Config.IndexClients] = new Client()
            {

                ID = rand.Next(1000, 9999),
                Name = "Jhon",
                Phone = "052-5647578",
                Latitude = 23.626,
                Longitude = -7.351
            };
            Config.IndexClients++;

            Clients[Config.IndexClients] = new Client()
            {

                ID = rand.Next(1000, 9999),
                Name = "Nathan",
                Phone = "052-6454521",
                Latitude = 11.230211,
                Longitude = 15.326545
            };
            Config.IndexClients++;

            Clients[Config.IndexClients] = new Client()
            {

                ID = rand.Next(1000, 9999),
                Name = "Haim",
                Phone = "052-9876545",
                Latitude = -12.546532,
                Longitude = 25.321654
            };
            Config.IndexClients++;

            Clients[Config.IndexClients] = new Client()
            {

                ID = rand.Next(1000, 9999),
                Name = "Yossef",
                Phone = "052-3215645",
                Latitude = 10.235645,
                Longitude = -9.2654321
            };
            Config.IndexClients++;

            Clients[Config.IndexClients] = new Client()
            {

                ID = rand.Next(1000, 9999),
                Name = "Uriel",
                Phone = "052-2645543",
                Latitude = 3.326554,
                Longitude = -10.354565
            };
            Config.IndexClients++;

            Clients[Config.IndexClients] = new Client()
            {

                ID = rand.Next(1000, 9999),
                Name = "Elhanan",
                Phone = "052-2365451",
                Latitude = 2.654987,
                Longitude = 26.326554
            };
            Config.IndexClients++;

            Clients[Config.IndexClients] = new Client()
            {

                ID = rand.Next(1000, 9999),
                Name = "Dana",
                Phone = "052-9889455",
                Latitude = 13.236545,
                Longitude = -2.654321
            };
            Config.IndexClients++;

            Clients[Config.IndexClients] = new Client()
            {

                ID = rand.Next(1000, 9999),
                Name = "Eliav",
                Phone = "052-1356846",
                Latitude = 13.564532,
                Longitude = 21.321654
            };
            Config.IndexClients++;

            Clients[Config.IndexClients] = new Client()
            {

                ID = rand.Next(1000, 9999),
                Name = "David",
                Phone = "052-9876556",
                Latitude = 36.3256412,
                Longitude = -15.321656
            };
            Config.IndexClients++;


            //init Package

            Packages[Config.IndexPackage] = new Package()
            {
                ID = rand.Next(100, 999),





            };













        }


    }







    public class DalObject
    {

        public DalObject() { DataSource.Initialize(); }



        public static void AddStation()
        {
           DataSource.Stations[DataSource.Config.IndexStations] = new Station()
           {


           }

        }

        
        
    }




}