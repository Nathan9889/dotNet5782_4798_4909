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

        
        public void Initialize()
        {
            //initialize Drone, 
            for (int i = 0; i < 5; i++)
            {
                Drones[i].ID = rand.Next(100, 1000);
                Drones[i].Model = "Mavic";
                Drones[i].MaxWeight = WeightCategories.medium ;
                Drones[i].Status = DroneStatus.Available;
                Drones[i].Battery = rand.Next(0, 100);
                Config.IndexStations++;
            }
           

        }


    }







    public class DalObject
    {



        
        
    }




}