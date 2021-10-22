using System;
using IDAL.DO;
using DalObject;


namespace ConsoleUI
{
    class Program
    {
        enum Menu { Add, Update, DisplayItem, DisplayList };
        enum ObjectMenu { Exit, Client, Drone, Station, Package }
        public static void Display()
        {
            int num = 1;
            Menu menu;
            ObjectMenu objectMenu;

            while (num != 0)
            {
                Console.WriteLine("Choose an Option: \n");
                Console.WriteLine(" 1: Add \n 2: Update \n 3: Display Item: \n 4: Display List \n 0: Exit");
                int choice = int.Parse(Console.ReadLine());
                const int f = 0;

                switch (choice)
                {

                    case 1:
                        Console.WriteLine("Choose an Adding Option: \n 1 : Client \n 2 : Drone \n 3 : Station: \n 4 : Package \n ");
                        objectMenu = (ObjectMenu)int.Parse(Console.ReadLine());

                        switch (objectMenu)
                        {
                            case ObjectMenu.Client:

                                Console.WriteLine("Enter Client Data\n");
                                int clientId = int.Parse(Console.ReadLine());
                                string clientName = Console.ReadLine();
                                string clientPhone = Console.ReadLine();
                                double clientLatitude = double.Parse(Console.ReadLine());
                                double clientLongitude = double.Parse(Console.ReadLine());

                                Client client = new Client();

                                client.ID = clientId;
                                client.Name = clientName;
                                client.Phone = clientPhone;
                                client.Latitude = clientLatitude;
                                client.Longitude = clientLongitude;

                                DalObject.DalObject.AddClient(client);

                                break;
                            case ObjectMenu.Drone:
                                Console.WriteLine("Enter Drone Data\n");
                                int droneId = int.Parse(Console.ReadLine());
                                string droneModel = Console.ReadLine();
                                Console.WriteLine("Choose Drone Weight: 0- Light, 1- Medium, 2- Heavy :\n");
                                string chosen = (Console.ReadLine());
                                WeightCategories droneWeight = (WeightCategories)Convert.ToInt32(chosen);
                                Console.WriteLine("Choose Drone Status: 0- Available, 1- Maintenance, 2- Shipping :\n");
                                chosen = (Console.ReadLine());
                                DroneStatus droneStatus = (DroneStatus)Convert.ToInt32(chosen);
                                double droneBattery = double.Parse(Console.ReadLine());

                                Drone drone = new Drone();
                                drone.ID = droneId;
                                drone.Model = droneModel;
                                drone.MaxWeight = droneWeight;
                                drone.Status = droneStatus;
                                drone.Battery = droneBattery;

                                DalObject.DalObject.AddDrone(drone);
                                break;

                            case ObjectMenu.Station:
                                Console.WriteLine("Enter Station Data\n");
                                int stationId = int.Parse(Console.ReadLine());
                                string stationName = Console.ReadLine();
                                int stationChargeSlot = int.Parse(Console.ReadLine());
                                double stationLatitude = double.Parse(Console.ReadLine());
                                double stationLongitude = double.Parse(Console.ReadLine());

                                Station station = new Station();
                                station.ID = stationId;
                                station.Name = stationName;
                                station.ChargeSlots = stationChargeSlot;
                                station.Longitude = stationLongitude;
                                station.Latitude = stationLatitude;


                                DalObject.DalObject.AddStation(station);




                                break;
                            case ObjectMenu.Package:
                                Console.WriteLine("Enter All Package Data\n");
                                int packageId = int.Parse(Console.ReadLine());
                                int packageSenderId = int.Parse(Console.ReadLine());
                                int packageTargetId = int.Parse(Console.ReadLine());
                                int packageDroneId = int.Parse(Console.ReadLine());
                                Console.WriteLine("Choose package Weight: 0- Light, 1- Medium, 2- Heavy :\n");
                                chosen = (Console.ReadLine());
                                WeightCategories packageWeight = (WeightCategories)Convert.ToInt32(chosen);
                                Console.WriteLine("Choose package Priority: 0- Standard, 1- Fast, 2- Urgent :\n");
                                chosen = (Console.ReadLine());
                                Priorities packagePriority = (Priorities)Convert.ToInt32(chosen);

                                Package package = new Package();

                                package.ID = packageId;
                                package.SenderId = packageSenderId;
                                package.TargetId = packageTargetId;
                                package.DroneId = packageDroneId;
                                package.Weight = packageWeight;
                                package.Priority = packagePriority;

                                DalObject.DalObject.AddPackage(package);



                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:


                        break;

                    case 3:


                        break;

                    case 4:


                        break;

                    case f:
                        num = 0;
                        break;

                    default:
                        Console.WriteLine("Invalid \n");
                        break;

                }


            }









        }



































        static void Main(string[] args)
        {

            //Console.WriteLine($"Name is {client.Name}, ID = {client.ID}");
            Display();

        }

        //private static void myClient()
        //{
        //    IDAL.DO.Client client = new IDAL.DO.Client
        //    {
        //        ID = 113,
        //        Name = "David",
        //        Latitude = 36.123456,
        //        Longitude = 29.654321,
        //        Phone = "0526137053"
        //    };
        //    Console.WriteLine(client);
        //}
    }
}
