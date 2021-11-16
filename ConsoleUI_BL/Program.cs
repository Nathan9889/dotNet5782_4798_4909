using System;
//Mini Project Targil 1:
//Name: Nathan Sayag, TZ: 328944798
//Name: Haim Goren, TZ: 207214909 

// BL CONSOLE



using IBL.BO;
using DalObject;

namespace ConsoleUI_BL
{
    class Program
    {

        ///Enum for for User Option
        enum Menu { Exit, Add, Update, DisplayItem, DisplayList, Distance };
        enum UpdateOptions { Exit, UpdateDronedata, UpdateStationData, UpdateClientData, Charging, FinishCharging, Assignment, PickedUp, Delivered };
        enum ObjectMenu { Exit, Client, Drone, Station, Package };
        enum ObjectList { Exit, ClientList, DroneList, StationList, PackageList, PackageWithoutDrone, StationWithCharging };
        enum DistanceOptions { Exit, Client, Station };

        /// <summary>
        /// Main function to run the program, the program get user input and display the relevant application from user choice, User can: Add An object, Update different type of information, Display specific object and Display every element from different list.
        /// </summary>
        public static void Display(IBL.IBL bl)
        {
            Menu choice;
            ObjectMenu objectMenu;
            UpdateOptions updateOptions;
            ObjectList objectList;
            DistanceOptions distanceOptions;
            int num = 1;

            while (num != 0)
            {
                Console.WriteLine("Choose an Option:");
                Console.WriteLine(" 1: Add \n 2: Update \n 3: Display specific Item \n 4: Display Item List \n 5: Distance \n 0: Exit");
                choice = (Menu)int.Parse(Console.ReadLine());    //User input to go through the menu

                switch (choice)
                {
                    case Menu.Add:  //Adding a new Object to the list of different object
                        {
                            Console.WriteLine("Choose an Adding Option: \n 1 : Station \n 2 : Drone \n 3 : Client: \n 4 : Package \n ");
                            objectMenu = (ObjectMenu)int.Parse(Console.ReadLine());

                            switch (objectMenu)
                            {
                                case ObjectMenu.Station:

                                    Console.WriteLine("Enter Station Data: Station ID, Station Name, Station location, Num of Available Stand  \n");  // Getting Station data from user

                                    int stationId;//
                                    int.TryParse(Console.ReadLine(), out stationId);
                                    string stationName = Console.ReadLine();//
                                    Location location = new Location();
                                    double myLatitude, myLongitude;
                                    double.TryParse(Console.ReadLine(), out myLatitude);
                                    double.TryParse(Console.ReadLine(), out myLongitude);
                                    location.Latitude = myLatitude;
                                    location.Longitude = myLongitude;
                                    int stationChargeSlot;
                                    int.TryParse(Console.ReadLine(), out stationChargeSlot);

                                    Station station = new Station();

                                    station.ID = stationId;
                                    station.Name = stationName;
                                    station.StationLocation = location;
                                    station.VacantChargeSlots = stationChargeSlot;

                                    station.ChargingDronesList = new System.Collections.Generic.List<ChargingDrone>();////have to checkk

                                    //need adding station in BL
                                    bl.AddStation(station);  //check
                                    
                                    break;

                                case ObjectMenu.Drone:

                                    Console.WriteLine("Enter Drone Data: Drone ID, Drone Model, Drone Max Weight, Station number \n");  // Getting Drone data from user   

                                   

                                    int droneId;
                                    int.TryParse(Console.ReadLine(), out droneId);

                                    //Console.WriteLine("Choose Drone Model: 0 :  Dji_Mavic_2_Pro, 1 : Dji_Mavic_2_Air, 2 : Dji_Mavic_2_Zoom, 3 :  Dji_FPV_Combo :\n");  //getting different type of Model from user
                                    string chosen = Console.ReadLine();  //used to get the num from user and chose with it different enum option
                                    string droneModel = chosen;

                                    Console.WriteLine("Choose Drone Weight: 0 : Light, 1 : Medium, 2 : Heavy :\n");  //getting different type of weight from user
                                    chosen = (Console.ReadLine());  //used to get the num from user and chose with it different enum option
                                    WeightCategories droneMaxWeight = (WeightCategories)Convert.ToInt32(chosen);

                                    int stationNumToCharge;
                                    int.TryParse(Console.ReadLine(), out stationNumToCharge);

                                    Drone drone = new Drone();
                                    Random rand = new Random();

                                    drone.ID = droneId;
                                    drone.Model = droneModel;
                                    drone.MaxWeight = droneMaxWeight;

                                    

                                    //BL
                                    //יוסף כשנמצא בתחזוקה???



                                    bl.AddDrone(drone, stationNumToCharge);

                                    //adding (drone) in bl


                                    break;

                                case ObjectMenu.Client:

                                    Console.WriteLine("Enter Client Data: ID, Name, Phone, Location\n");   // Getting Client data from user

                                    

                                    int clientId;
                                    int.TryParse(Console.ReadLine(), out clientId);
                                    string clientName = Console.ReadLine();
                                    string clientPhone = Console.ReadLine();

                                    Location myClientLocation = new Location();
                                    Console.WriteLine("Enter Latitude and Longitude \n");

                                    double.TryParse(Console.ReadLine(), out myLatitude);
                                    double.TryParse(Console.ReadLine(), out myLongitude);
                                    myClientLocation.Latitude = myLatitude;
                                    myClientLocation.Longitude = myLongitude;

                                    Client client = new Client();

                                    client.ID = clientId;
                                    client.Name = clientName;
                                    client.Phone = clientPhone;
                                    client.ClientLocation = myClientLocation;


                                    //adding (client) in bl

                                    break;

                                case ObjectMenu.Package:
                                    Console.WriteLine("Enter All Package Data: SenderId, ReceiverId, MaxWeight, Priority,");  // Getting Package data from user

                                    int packageSenderId, packageTargetId;
                                    int.TryParse(Console.ReadLine(), out packageSenderId);
                                    int.TryParse(Console.ReadLine(), out packageTargetId);

                                    Console.WriteLine("Choose package Weight: 0 : Light, 1 : Medium, 2 : Heavy :");
                                    chosen = (Console.ReadLine());
                                    WeightCategories packageWeight = (WeightCategories)Convert.ToInt32(chosen);
                                    Console.WriteLine("Choose package Priority: 0 :  Standard, 1 : Fast, 2 : Urgent :");
                                    chosen = (Console.ReadLine());
                                    Priorities packagePriority = (Priorities)Convert.ToInt32(chosen);


                                    Package package = new Package();

                                    ClientPackage senderClient = new ClientPackage(); //bdika
                                    ClientPackage receiverClient = new ClientPackage();
                                    senderClient.ID = packageSenderId;
                                    receiverClient.ID = packageTargetId;

                                    package.SenderClient = senderClient;
                                    package.ReceiverClient = receiverClient;
                                    package.Weight = packageWeight;
                                    package.Priority = packagePriority;

                                    //adding package in BL
                                    bl.addPackage(package);
                                    
                                    break;

                                default:
                                    break;
                            }




                            break;
                        }

                    case Menu.Update:   //Update item
                        {

                            Console.WriteLine("Choose an Option:");
                            Console.WriteLine(" 1: Update Drone Model \n 2: Update Station Data \n 3: Update Client Data : \n 4: Send Drone to Charge \n 5: Finish charging drone \n 6: Assigning a package to a drone \n 7: Pick Up Package by Drone \n 8: Delivery of a package to the client: \n  0: Exit");  ///User Choose Different type of Update
                            updateOptions = (UpdateOptions)int.Parse(Console.ReadLine());

                            switch (updateOptions)
                            {
                                case UpdateOptions.Exit:
                                    break;
                                case UpdateOptions.UpdateDronedata:      //New Model name

                                    Console.WriteLine("Enter Drone Id and New Drone Model name");
                                    int droneIdUp;
                                    int.TryParse(Console.ReadLine(), out droneIdUp);
                                    ///
                                    ///
                                    ///

                                    break;

                                case UpdateOptions.UpdateStationData:  //
                                    
                                    break;

                                case UpdateOptions.UpdateClientData:   //
                                    
                                    break;

                                case UpdateOptions.Charging:    //sending a drone to a station to get it charged
                                   
                                    break;

                                case UpdateOptions.FinishCharging:  //Getting a drone back from charging
                                   
                                    break;
                                case UpdateOptions.Assignment:  //Assign Package to a drone using Drone and package ID.

                                    break;

                                case UpdateOptions.PickedUp:    //Getting a drone to pick up a package 
                                   
                                    break;

                                case UpdateOptions.Delivered:   //Deliver a Package to a client
                                   
                                    break;

                                default:
                                    break;
                            }
                            break;
                        }
                    case Menu.DisplayItem:   // Output Specific item Data
                        {




                            break;
                        }
                    case Menu.DisplayList:   // Output all list of different object
                        {
                            



                            break;
                        }
                    case Menu.Distance:
                        {



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

            IBL.IBL bl = new BL.BL();
            Display(bl);

            Console.WriteLine("Hello World!");
        }




    }
}
