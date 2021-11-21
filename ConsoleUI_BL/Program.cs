using System;
//Mini Project Targil 2:
//Name: Nathan Sayag, TZ: 328944798
//Name: Haim Goren, TZ: 207214909 

using IBL.BO;

namespace ConsoleUI_BL
{
    class Program
    {

        ///Enum for for User Option
        enum Menu { Exit, Add, Update, DisplayItem, DisplayList, Distance };
        enum UpdateOptions { Exit, UpdateDronedata, UpdateStationData, UpdateClientData, Charging, FinishCharging, Assignment, PickedUp, Delivered };
        enum ObjectMenu { Exit, Station, Drone, Client, Package };
        enum ObjectList { Exit, StationList, DroneList, ClientList, PackageList, PackageListWithoutDrone, StationListWithCharging };
        //enum DistanceOptions { Exit, Client, Station };

        /// <summary>
        /// Main function to run the program, the program get user input and display the relevant application from user choice, User can: Add An object, Update different type of information, Display specific object and Display every element from different list.
        /// </summary>
        public static void Display(IBL.IBL bl)
        {
            Menu choice;
            ObjectMenu objectMenu;
            UpdateOptions updateOptions;
            ObjectList objectList;
            //DistanceOptions distanceOptions;
            int num = 1;

            while (num != 0)
            {
                Console.WriteLine("Choose an Option:");
                Console.WriteLine(" 1: Add \n 2: Update \n 3: Display specific Item \n 4: Display Item List \n 0: Exit");
                choice = (Menu)int.Parse(Console.ReadLine());    //User input to go through the menu

                try
                {
                    switch (choice)
                    {
                        case Menu.Add:  //Adding a new Object to the list of different object
                            {
                                Console.WriteLine("Choose an Adding Option: \n 1: Station \n 2: Drone \n 3: Client: \n 4: Package ");
                                objectMenu = (ObjectMenu)int.Parse(Console.ReadLine());

                                switch (objectMenu)
                                {
                                    case ObjectMenu.Station:

                                        Console.WriteLine("Enter Station Data: Station ID, Station Name, Station location, Num of Available Stand  \n");  // Getting Station data from user

                                        int stationId;
                                        int.TryParse(Console.ReadLine(), out stationId);
                                        string stationName = Console.ReadLine();//
                                        Console.WriteLine("Enter Latitude and Longitude of the station\n");
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
                                        station.AvailableChargeSlots = stationChargeSlot;//check
                                        station.ChargingDronesList = new System.Collections.Generic.List<ChargingDrone>();////have to checkk

                                        bl.AddStation(station);

                                        //if no Charigot
                                        Console.WriteLine("Station added succesfully !");

                                        break;

                                    case ObjectMenu.Drone:

                                        Console.WriteLine("Enter Drone Data: Drone ID, Drone Model, Drone Max Weight, Station number ");  // Getting Drone data from user   

                                        int droneId;
                                        int.TryParse(Console.ReadLine(), out droneId);
                                        string droneModel = Console.ReadLine();
                                        string chosen;
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


                                        bl.AddDrone(drone, stationNumToCharge);
                                        Console.WriteLine("Drone added succesfully !");



                                        break;

                                    case ObjectMenu.Client:

                                        Console.WriteLine("Enter Client Data: ID, Name, Phone, Location");   // Getting Client data from user


                                        int clientId;
                                        int.TryParse(Console.ReadLine(), out clientId);
                                        string clientName = Console.ReadLine();
                                        string clientPhone = Console.ReadLine();
                                        Location myClientLocation = new Location();
                                        Console.WriteLine("Enter Latitude and Longitude of the client \n");
                                        double.TryParse(Console.ReadLine(), out myLatitude);
                                        double.TryParse(Console.ReadLine(), out myLongitude);
                                        myClientLocation.Latitude = myLatitude;
                                        myClientLocation.Longitude = myLongitude;

                                        Client client = new Client();

                                        client.ID = clientId;
                                        client.Name = clientName;
                                        client.Phone = clientPhone;
                                        client.ClientLocation = myClientLocation;

                                        bl.AddClient(client);


                                        Console.WriteLine("Client added succesfully !");

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
                                        package.TargetClient = receiverClient;
                                        package.Weight = packageWeight;
                                        package.Priority = packagePriority;


                                        int id =  bl.AddPackage(package);
                                        
                                        Console.WriteLine($"Package added succesfully ! the id is {id} ");
                                        break;

                                    default:
                                        break;
                                }
                                break;
                            }

                        case Menu.Update:   //Update item
                            {
                                Console.WriteLine("Choose an Option:");
                                Console.WriteLine(" 1: Update Drone Data \n 2: Update Station Data \n 3: Update Client Data : \n 4: Send Drone to Charge \n 5: Finish charging drone \n " +
                                                  "6: Assigning a package to a drone \n 7: Pick Up Package by Drone \n 8: Delivery of a package to the client: \n  0: Exit");  ///User Choose Different type of Update
                                updateOptions = (UpdateOptions)int.Parse(Console.ReadLine());

                                switch (updateOptions)
                                {
                                    case UpdateOptions.Exit:
                                        break;

                                    case UpdateOptions.UpdateDronedata:      //New Model name

                                        Console.WriteLine("Enter Drone Id");
                                        int droneIdUp;

                                        Console.WriteLine("Enter new Drone Model name");
                                        int.TryParse(Console.ReadLine(), out droneIdUp);
                                        string name = Console.ReadLine();

                                        bl.UpdateDroneName(droneIdUp, name);
                                        Console.WriteLine("Update Drone succesfully !");

                                        break;

                                    case UpdateOptions.UpdateStationData:  //
                                        Console.WriteLine("Enter Station Id");
                                        int stationId;
                                        int.TryParse(Console.ReadLine(), out stationId);

                                        Console.WriteLine("Enter new station name");
                                        string stationName = Console.ReadLine();
                                        Console.WriteLine("Enter number of charge stand (optional)");
                                        int numOfCharge;
                                        bool f = int.TryParse(Console.ReadLine(), out numOfCharge);
                                        

                                        bl.UpdateStation(stationId, stationName, numOfCharge);
                                        Console.WriteLine("Update Station succesfully !");

                                        break;

                                    case UpdateOptions.UpdateClientData:   //

                                        Console.WriteLine("Enter Client Id");
                                        int clientId;
                                        int.TryParse(Console.ReadLine(), out clientId);
                                        Console.WriteLine("Enter new Client Name ");
                                        string clientName = Console.ReadLine();
                                        Console.WriteLine("Enter new Client phone");
                                        string clientPhone = Console.ReadLine();

                                        bl.UpdateClient(clientId, clientName, clientPhone);
                                        Console.WriteLine("Update Client succesfully !");

                                        break;

                                    case UpdateOptions.Charging:    //sending a drone to a station to get it charged

                                        Console.WriteLine("Enter Drone Id to charge");
                                        int droneIdToCharge;
                                        int.TryParse(Console.ReadLine(), out droneIdToCharge);

                                        bl.ChargeDrone(droneIdToCharge);
                                        break;

                                    case UpdateOptions.FinishCharging:  //Getting a drone back from charging

                                        Console.WriteLine("Enter Drone Id to finish Charging");
                                        int droneIdToFinish;
                                        int.TryParse(Console.ReadLine(), out droneIdToFinish);

                                        Console.WriteLine("Enter the charging minutes");
                                        double timeCharging;
                                        double.TryParse(Console.ReadLine(), out timeCharging);

                                        bl.FinishCharging(droneIdToFinish, timeCharging);
                                        break;

                                    case UpdateOptions.Assignment:  //Assign Package to a drone using Drone and package ID.
                                        Console.WriteLine("Enter Drone Id to to assign it a package");
                                        int droneIdToAssign;
                                        int.TryParse(Console.ReadLine(), out droneIdToAssign);

                                        bl.packageToDrone(droneIdToAssign);
                                        break;

                                    case UpdateOptions.PickedUp:    //Getting a drone to pick up a package 
                                        Console.WriteLine("Enter Drone Id to to PickedUp a package");
                                        int droneIdToPickedUp;
                                        int.TryParse(Console.ReadLine(), out droneIdToPickedUp);

                                        bl.PickedUpByDrone(droneIdToPickedUp);
                                        break;

                                    case UpdateOptions.Delivered:   //Deliver a Package to a client
                                        Console.WriteLine("Enter Drone Id to to deliver a package");
                                        int droneIdToDeliver;
                                        int.TryParse(Console.ReadLine(), out droneIdToDeliver);

                                        bl.DeliveredToClient(droneIdToDeliver);
                                        break;

                                    default:
                                        break;
                                }
                                break;
                            }
                        case Menu.DisplayItem:   // Output Specific item Data
                            {
                                Console.WriteLine("Choose which Item to display: \n 1: Station\n 2: Drone \n 3: Client \n 4: Package \n 0: Exit ");
                                objectMenu = (ObjectMenu)int.Parse(Console.ReadLine());
                                switch (objectMenu)
                                {
                                    case ObjectMenu.Exit:
                                        break;

                                    case ObjectMenu.Station:
                                        Console.WriteLine("What is the station's ID?");
                                        int stationId;
                                        int.TryParse(Console.ReadLine(), out stationId);

                                        Console.WriteLine(bl.DisplayStation(stationId));
                                        break;

                                    case ObjectMenu.Drone:
                                        Console.WriteLine("What is the drone's ID");
                                        int droneId;
                                        int.TryParse(Console.ReadLine(), out droneId);

                                        Console.WriteLine(bl.DisplayDrone(droneId));
                                        break;

                                    case ObjectMenu.Client:
                                        Console.WriteLine("What is the client's ID?");
                                        int clientId;
                                        int.TryParse(Console.ReadLine(), out clientId);

                                        Console.WriteLine(bl.DisplayClient(clientId));
                                        break;

                                    case ObjectMenu.Package:
                                        Console.WriteLine("What is the package's ID?");
                                        int packageId;
                                        int.TryParse(Console.ReadLine(), out packageId);

                                        Console.WriteLine(bl.DisplayPackage(packageId));
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            }


                        case Menu.DisplayList:   // Output all list of different object
                            {
                                Console.WriteLine("Choose which Item List to display: \n 1: Station list\n 2: Drone list \n 3: Client list \n 4: Package list \n 5: List of packages not associated to drone\n 6: List of stations with available charging slots\n 0: Exit ");
                                objectList = (ObjectList)int.Parse(Console.ReadLine());

                                switch (objectList)
                                {
                                    case ObjectList.Exit:
                                        break;

                                    case ObjectList.StationList:
                                        foreach (var station in bl.DisplayStationList())
                                        {
                                            Console.WriteLine(station);
                                        }
                                        break;

                                    case ObjectList.DroneList:
                                        foreach (var drone in bl.DisplayDroneList())
                                        {
                                            Console.WriteLine(drone);
                                        }
                                        break;

                                    case ObjectList.ClientList:
                                        foreach (var client in bl.DisplayClientList())
                                        {
                                            Console.WriteLine(client);
                                        }
                                        break;

                                    case ObjectList.PackageList:
                                        foreach (var package in bl.DisplayPackageList())
                                        {
                                            Console.WriteLine(package);
                                        }
                                        break;

                                    case ObjectList.PackageListWithoutDrone:
                                        foreach (var PackageWithoutDrone in bl.DisplayPackageListWithoutDrone())
                                        {
                                            Console.WriteLine(PackageWithoutDrone);
                                        }
                                        break;

                                    case ObjectList.StationListWithCharging:
                                        foreach (var StationWithCharging in bl.DisplayStationListWitAvailableChargingSlots())
                                        {
                                            Console.WriteLine(StationWithCharging);
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
            }
        }

        static void Main(string[] args)
        {
            IBL.IBL bl = new BL.BL();
            Display(bl);
        }
    }
}
