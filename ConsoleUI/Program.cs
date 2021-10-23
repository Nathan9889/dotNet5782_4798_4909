using System;
using IDAL.DO;
using DalObject;


namespace ConsoleUI
{
    class Program
    {
        enum Menu {Exit, Add, Update, DisplayItem, DisplayList };
        enum UpdateOptions { Exit, Assignment, PickedUp, Delivered, Charging, FinishCharging };
        enum ObjectMenu { Exit, Client, Drone, Station, Package };
        
        enum ObjectList { Exit, ClientList, DroneList, StationList, PackageList, PackageWithoutDrone, StationWithCharging };

        public static void Display()
        {
            int num = 1;
            Menu choice;
            ObjectMenu objectMenu;
            UpdateOptions updateOptions;
            ObjectList objectList;

            while (num != 0)
            {
                Console.WriteLine("Choose an Option: \n");
                Console.WriteLine(" 1: Add \n 2: Update \n 3: Display specific Item \n 4: Display Item List \n 0: Exit");
                choice = (Menu)int.Parse(Console.ReadLine());
            

                switch (choice)
                {

                    case Menu.Add:

                        Console.WriteLine("Choose an Adding Option: \n 1 : Client \n 2 : Drone \n 3 : Station: \n 4 : Package \n ");
                        objectMenu = (ObjectMenu)int.Parse(Console.ReadLine());

                        switch (objectMenu)
                        {
                            case ObjectMenu.Client:
                                
                                Console.WriteLine("Enter Client Data: ID, Name, Phone, Latitude, Longitude  \n");
                                int clientId = Convert.ToInt32(Console.ReadLine());
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

                                Console.WriteLine("Enter Drone Data: ID, Model, Weight, Status, Battery \n");
                                int droneId = int.Parse(Console.ReadLine());
                                string droneModel = Console.ReadLine();
                                Console.WriteLine("Choose Drone Weight: 0 : Light, 1 : Medium, 2 : Heavy :\n");
                                string chosen = (Console.ReadLine());
                                WeightCategories droneWeight = (WeightCategories)Convert.ToInt32(chosen);
                                Console.WriteLine("Choose Drone Status: 0 : Available, 1 : Maintenance, 2 : Shipping :\n");
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

                                Console.WriteLine("Enter Station Data: ID, Name, Num of ChargingSlot, Longitude, Latitude\n");
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
                                Console.WriteLine("Enter All Package Data: ID, SenderId, TargetId, DroneId, MaxWeight, Priority\n");
                                int packageId = int.Parse(Console.ReadLine());
                                int packageSenderId = int.Parse(Console.ReadLine());
                                int packageTargetId = int.Parse(Console.ReadLine());
                                int packageDroneId = int.Parse(Console.ReadLine());
                                Console.WriteLine("Choose package Weight: 0 : Light, 1 : Medium, 2 : Heavy :\n");
                                chosen = (Console.ReadLine());
                                WeightCategories packageWeight = (WeightCategories)Convert.ToInt32(chosen);
                                Console.WriteLine("Choose package Priority: 0 :  Standard, 1 : Fast, 2 : Urgent :\n");
                                chosen = (Console.ReadLine());
                                Priorities packagePriority = (Priorities)Convert.ToInt32(chosen);
                                

                                Package package = new Package();

                                package.ID = packageId;
                                package.SenderId = packageSenderId;
                                package.TargetId = packageTargetId;
                                package.DroneId = packageDroneId;
                                package.Weight = packageWeight;
                                package.Priority = packagePriority;
                                package.Created = DateTime.Now;

                                DalObject.DalObject.AddPackage(package);

                                break;
                            default:
                                break;
                        }
                        break;

                    case Menu.Update:
                        Console.WriteLine("Choose an Option: \n");
                        Console.WriteLine(" 1: Assigning a package to a drone \n 2: Pick Up Package by Drone \n 3: Delivery of a package to the client: \n 4: Charging drone \n 5: Finish charging drone \n 0: Exit");
                        updateOptions = (UpdateOptions)int.Parse(Console.ReadLine());

                        switch (updateOptions)
                        {
                            case UpdateOptions.Exit:
                                break;
                            case UpdateOptions.Assignment:
                                int droneId, packageId;
                                Console.WriteLine("What is the drone's ID?");
                                droneId = int.Parse(Console.ReadLine());
                                Console.WriteLine("What is the package's ID?");
                                packageId = int.Parse(Console.ReadLine());
                                DalObject.DalObject.packageToDrone(DalObject.DalObject.PackageById(packageId) , DalObject.DalObject.DroneById(droneId));
                                break;

                            case UpdateOptions.PickedUp:
                                Console.WriteLine("What is the package's ID?");
                                packageId = int.Parse(Console.ReadLine());
                                DalObject.DalObject.PickedUpByDrone(DalObject.DalObject.PackageById(packageId));
                                break;

                            case UpdateOptions.Delivered:
                                Console.WriteLine("What is the package's ID?");
                                packageId = int.Parse(Console.ReadLine());
                                DalObject.DalObject.DeliveredToClient(DalObject.DalObject.PackageById(packageId));
                                break;

                            case UpdateOptions.Charging:
                                Console.WriteLine("What is the drone's ID?");
                                droneId = int.Parse(Console.ReadLine());
                                Console.WriteLine("At which station do you want to recharge the drone?\n");
                                foreach (var station in (DalObject.DalObject.StationWithCharging())) 
                                {
                                    Console.WriteLine(station);
                                }
                                Console.WriteLine("What is the station ID ?\n");
                                int stationID = int.Parse(Console.ReadLine());
                                DalObject.DalObject.DroneCharge(DalObject.DalObject.DroneById(droneId), stationID);
                                break;

                            case UpdateOptions.FinishCharging:
                                Console.WriteLine("What is the drone's ID?");
                                droneId = int.Parse(Console.ReadLine());
                                DalObject.DalObject.FinishCharging(DalObject.DalObject.DroneChargeByIdDrone(droneId));
                                break;

                            default:
                                break;
                        }
                        break;

                    case Menu.DisplayItem:

                        Console.WriteLine("Choose the show option: \n 1: Client \n 2: Drone \n 3: Station \n 4: Package \n 0: Exit ");
                        objectMenu = (ObjectMenu)int.Parse(Console.ReadLine());
                        switch (objectMenu)
                        {
                            case ObjectMenu.Exit:
                                break;

                            case ObjectMenu.Client:
                                Console.WriteLine("What is the client's ID?");
                                int clientID = int.Parse(Console.ReadLine());
                                Console.WriteLine(DalObject.DalObject.ClientById(clientID));
                                break;

                            case ObjectMenu.Drone:
                                Console.WriteLine("What is the drone's ID?");
                                int droneID = int.Parse(Console.ReadLine());
                                Console.WriteLine(DalObject.DalObject.DroneById(droneID));
                                break;

                            case ObjectMenu.Station:
                                Console.WriteLine("What is the station's ID?");
                                int stationID = int.Parse(Console.ReadLine());
                                Console.WriteLine(DalObject.DalObject.StationById(stationID));
                                break;

                            case ObjectMenu.Package:
                                Console.WriteLine("What is the package's ID?");
                                int packageID = int.Parse(Console.ReadLine());
                                Console.WriteLine(DalObject.DalObject.PackageById(packageID));
                                break;

                            default:
                                break;
                        }
                        break;

                    case Menu.DisplayList:

                        Console.WriteLine("Choose the show option: \n 1: Clients list \n 2: Drones list\n 3: Stations list \n 4: Packages list\n 5: List of packages that do not belong to the drone \n 6: List of stations with available charging slots \n 0: Exit ");
                        objectList = (ObjectList)int.Parse(Console.ReadLine());
                        switch (objectList)
                        {
                            case ObjectList.Exit:
                                break;

                            case ObjectList.ClientList:
                                foreach (var client in DalObject.DalObject.ClientsList())
                                {
                                    Console.WriteLine(client);
                                }
                                break;

                            case ObjectList.DroneList:
                                foreach (var drone in DalObject.DalObject.DroneList())
                                {
                                    Console.WriteLine(drone);
                                }
                                break;

                            case ObjectList.StationList:
                                foreach (var station in DalObject.DalObject.StationsList())
                                {
                                    Console.WriteLine(station);
                                }
                                break;

                            case ObjectList.PackageList:
                                foreach (var package in DalObject.DalObject.PackageList())
                                {
                                    Console.WriteLine(package);
                                }
                                break;

                            case ObjectList.PackageWithoutDrone:
                                foreach (var package in DalObject.DalObject.PackageWithoutDrone())
                                {
                                    Console.WriteLine(package);
                                }
                                break;

                            case ObjectList.StationWithCharging:
                                foreach (var station in DalObject.DalObject.StationWithCharging())
                                {
                                    Console.WriteLine(station);
                                }
                                break;

                            default:
                                break;
                        }
                        break;

                    default:
                        break;

                }
            }
        }

        static void Main(string[] args)
        {
            new DalObject.DalObject();
            Display();
        }

    }
}
