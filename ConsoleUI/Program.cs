//Name: Nathan Sayag, TZ: 328944798
//    : Haim 
using System;
using IDAL.DO;
using DalObject;


namespace ConsoleUI
{
    class Program
    {
        ///Enum for for User Option
        enum Menu { Exit, Add, Update, DisplayItem, DisplayList };
        enum UpdateOptions { Exit, Assignment, PickedUp, Delivered, Charging, FinishCharging };
        enum ObjectMenu { Exit, Client, Drone, Station, Package };
        enum ObjectList { Exit, ClientList, DroneList, StationList, PackageList, PackageWithoutDrone, StationWithCharging };

        /// <summary>
        /// Main function to run the program, the program get user input and display the relevant application from user choice, User can: Add An object, Update different type of information, Display specific object and Display every element from different list.
        /// </summary>
        public static void Display()
        {
            Menu choice;
            ObjectMenu objectMenu;
            UpdateOptions updateOptions;
            ObjectList objectList;
            int num = 1;
             
            while (num != 0) 
            {
                Console.WriteLine("Choose an Option:");
                Console.WriteLine(" 1: Add \n 2: Update \n 3: Display specific Item \n 4: Display Item List \n 0: Exit");
                choice = (Menu)int.Parse(Console.ReadLine());    //User input to go through the menu

                switch (choice)
                {
                    case Menu.Add:  //Adding a new Object to the list of different object
                        {
                            Console.WriteLine("Choose an Adding Option: \n 1 : Client \n 2 : Drone \n 3 : Station: \n 4 : Package \n ");
                            objectMenu = (ObjectMenu)int.Parse(Console.ReadLine());

                            switch (objectMenu)
                            {
                                case ObjectMenu.Client:

                                    Console.WriteLine("Enter Client Data: ID, Name, Phone, Latitude, Longitude  \n");  // Getting Client data from user
                                    int clientId;
                                    int.TryParse(Console.ReadLine(), out clientId);
                                    string clientName = Console.ReadLine();
                                    string clientPhone = Console.ReadLine();
                                    double clientLatitude, clientLongitude;
                                    double.TryParse(Console.ReadLine(),out clientLatitude);
                                    double.TryParse(Console.ReadLine(), out clientLongitude);

                                    Client client = new Client();   //creating new object then assigning user input to that object

                                    client.ID = clientId;
                                    client.Name = clientName;
                                    client.Phone = clientPhone;
                                    client.Latitude = clientLatitude;
                                    client.Longitude = clientLongitude;

                                    DalObject.DalObject.AddClient(client);  // Adding the new object to the list of that object
                                    break;

                                case ObjectMenu.Drone:

                                    Console.WriteLine("Enter Drone Data: ID, Model, Weight, Status, Battery \n");  // Getting Drone data from user
                                    int droneId;
                                    int.TryParse(Console.ReadLine(), out droneId);
                                    string droneModel = Console.ReadLine();
                                    Console.WriteLine("Choose Drone Weight: 0 : Light, 1 : Medium, 2 : Heavy :\n");  //getting different type of weight from user
                                    string chosen = (Console.ReadLine());  //used to get the num from user and chose with it different enum option
                                    WeightCategories droneWeight = (WeightCategories)Convert.ToInt32(chosen);
                                    Console.WriteLine("Choose Drone Status: 0 : Available, 1 : Maintenance, 2 : Shipping :\n"); // For different type of status from user
                                    chosen = (Console.ReadLine());
                                    DroneStatus droneStatus = (DroneStatus)Convert.ToInt32(chosen);
                                    double droneBattery;
                                    double.TryParse(Console.ReadLine(),out droneBattery);

                                    Drone drone = new Drone();      //creating new object then assigning user input to that object

                                    drone.ID = droneId;
                                    drone.Model = droneModel;
                                    drone.MaxWeight = droneWeight;
                                    drone.Status = droneStatus;
                                    drone.Battery = droneBattery;

                                    DalObject.DalObject.AddDrone(drone);   // Adding the new object to the list of that object
                                    break;

                                case ObjectMenu.Station:

                                    Console.WriteLine("Enter Station Data: ID, Name, Num of ChargingSlot, Longitude, Latitude\n");   // Getting Station data from user
                                    int stationId;
                                    int.TryParse(Console.ReadLine(), out stationId);
                                    string stationName = Console.ReadLine();
                                    int stationChargeSlot = int.Parse(Console.ReadLine());
                                    double stationLatitude, stationLongitude;
                                    double.TryParse(Console.ReadLine(), out stationLatitude);
                                    double.TryParse(Console.ReadLine(), out stationLongitude);

                                    Station station = new Station();  //creating new object then assigning user input to that object

                                    station.ID = stationId;
                                    station.Name = stationName;
                                    station.ChargeSlots = stationChargeSlot;
                                    station.Longitude = stationLongitude;
                                    station.Latitude = stationLatitude;

                                    DalObject.DalObject.AddStation(station);        // Adding the new object to the list of that object
                                    break;

                                case ObjectMenu.Package:
                                    Console.WriteLine("Enter All Package Data: ID, SenderId, TargetId, DroneId, MaxWeight, Priority\n");  // Getting Package data from user
                                    int packageId, packageSenderId, packageTargetId,packageDroneId ;
                                    int.TryParse(Console.ReadLine(), out packageId);
                                    int.TryParse(Console.ReadLine(), out packageSenderId);
                                    int.TryParse(Console.ReadLine(), out packageTargetId);
                                    int.TryParse(Console.ReadLine(), out packageDroneId);

                                    Console.WriteLine("Choose package Weight: 0 : Light, 1 : Medium, 2 : Heavy :\n");
                                    chosen = (Console.ReadLine());
                                    WeightCategories packageWeight = (WeightCategories)Convert.ToInt32(chosen);
                                    Console.WriteLine("Choose package Priority: 0 :  Standard, 1 : Fast, 2 : Urgent :\n");
                                    chosen = (Console.ReadLine());
                                    Priorities packagePriority = (Priorities)Convert.ToInt32(chosen);

                                    Package package = new Package();   //creating new object then assigning user input to that object

                                    package.ID = packageId;
                                    package.SenderId = packageSenderId;
                                    package.TargetId = packageTargetId;
                                    package.DroneId = packageDroneId;
                                    package.Weight = packageWeight;
                                    package.Priority = packagePriority;
                                    package.Created = DateTime.Now;

                                    DalObject.DalObject.AddPackage(package); // Adding the new object to the list of that object
                                    break;

                                default:
                                    break;
                            }
                            break;
                        }

                    case Menu.Update:   //Update item
                        {
                            Console.WriteLine("Choose an Option: \n");
                            Console.WriteLine(" 1: Assigning a package to a drone \n 2: Pick Up Package by Drone \n 3: Delivery of a package to the client: \n 4: Charging drone \n 5: Finish charging drone \n 0: Exit");  ///User Choose Different type of Update
                            updateOptions = (UpdateOptions)int.Parse(Console.ReadLine());

                            switch (updateOptions)
                            {
                                case UpdateOptions.Exit:
                                    break;
                                case UpdateOptions.Assignment:  //Assign Package to a drone using Drone and package ID.
                                    int droneId, packageId;
                                    Console.WriteLine("What is the drone's ID?");
                                    int.TryParse(Console.ReadLine(), out droneId);
                                    Console.WriteLine("What is the package's ID?");
                                    int.TryParse(Console.ReadLine(), out packageId);
                                    DalObject.DalObject.packageToDrone(DalObject.DalObject.PackageById(packageId), DalObject.DalObject.DroneById(droneId));  //Getting ID input then sending the ID inputed to Dalobject method packagebyId and droneById that return the items who match the Id's then put both items in packageToDrone method
                                    break;

                                case UpdateOptions.PickedUp:    //Getting a drone to pick up a package 
                                    Console.WriteLine("What is the package's ID?");
                                    int.TryParse(Console.ReadLine(), out packageId);
                                    DalObject.DalObject.PickedUpByDrone(DalObject.DalObject.PackageById(packageId));  // Same as previous
                                    break;

                                case UpdateOptions.Delivered:   //Deliver a Package to a client
                                    Console.WriteLine("What is the package's ID?");
                                    int.TryParse(Console.ReadLine(), out packageId);
                                    DalObject.DalObject.DeliveredToClient(DalObject.DalObject.PackageById(packageId));  
                                    break;

                                case UpdateOptions.Charging:    //sending a drone to a station to get it charged
                                    Console.WriteLine("What is the drone's ID?");
                                    int.TryParse(Console.ReadLine(), out droneId);
                                    Console.WriteLine("At which station do you want to recharge the drone?\n");
                                    foreach (var station in (DalObject.DalObject.StationWithCharging()))  // Display the stations list who have places to charge
                                    {
                                        Console.WriteLine(station);
                                    }
                                    Console.WriteLine("What is the station ID ?\n");
                                    int stationId;
                                    int.TryParse(Console.ReadLine(), out stationId);
                                    DalObject.DalObject.DroneCharge(DalObject.DalObject.DroneById(droneId), stationId);
                                    break;

                                case UpdateOptions.FinishCharging:  //Getting a drone back from charging
                                    Console.WriteLine("What is the drone's ID?");
                                    int.TryParse(Console.ReadLine(), out droneId);
                                    DalObject.DalObject.FinishCharging(DalObject.DalObject.DroneChargeByIdDrone(droneId));
                                    break;

                                default:
                                    break;
                            }
                            break;
                        }
                    case Menu.DisplayItem:   // Output Specific item Data
                        {
                            Console.WriteLine("Choose Which Item data to display: \n 1: Client \n 2: Drone \n 3: Station \n 4: Package \n 0: Exit ");
                            objectMenu = (ObjectMenu)int.Parse(Console.ReadLine());
                            switch (objectMenu)
                            {
                                case ObjectMenu.Exit:
                                    break;

                                case ObjectMenu.Client:
                                    Console.WriteLine("What is the client's ID?");
                                    int clientId;
                                    int.TryParse(Console.ReadLine(), out clientId);
                                    Console.WriteLine(DalObject.DalObject.ClientById(clientId));  //output the tostring func of client object that match the id user inputed
                                    break;

                                case ObjectMenu.Drone:
                                    Console.WriteLine("What is the drone's ID?");
                                    int droneId;
                                    int.TryParse(Console.ReadLine(), out droneId);
                                    Console.WriteLine(DalObject.DalObject.DroneById(droneId));   //same for rest
                                    break;

                                case ObjectMenu.Station:
                                    Console.WriteLine("What is the station's ID?");
                                    int stationId;
                                    int.TryParse(Console.ReadLine(), out stationId);
                                    Console.WriteLine(DalObject.DalObject.StationById(stationId));
                                    break;

                                case ObjectMenu.Package:
                                    Console.WriteLine("What is the package's ID?");
                                    int packageId;
                                    int.TryParse(Console.ReadLine(), out packageId);
                                    Console.WriteLine(DalObject.DalObject.PackageById(packageId));
                                    break;

                                default:
                                    break;
                            }
                            break;
                        }
                    case Menu.DisplayList:   // Output all list of different object
                        {
                            Console.WriteLine("Choose the show option: \n 1: Clients list \n 2: Drones list\n 3: Stations list \n 4: Packages list\n 5: List of packages that do not belong to the drone \n 6: List of stations with available charging slots \n 0: Exit ");
                            objectList = (ObjectList)int.Parse(Console.ReadLine());
                            switch (objectList)
                            {
                                case ObjectList.Exit:
                                    break;

                                case ObjectList.ClientList:
                                    foreach (var client in DalObject.DalObject.ClientsList())  // Display every element in Client list, same for all
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
                        }

                    case Menu.Exit:
                        num = 0;
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
