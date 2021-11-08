using System;
//Mini Project Targil 1:
//Name: Nathan Sayag, TZ: 328944798
//Name: Haim Goren, TZ: 207214909 

// BL CONSOLE



using IDAL.DO;
using DalObject;

namespace ConsoleUI_BL
{


    class Program
    {
        ///Enum for for User Option
        enum Menu { Exit, Add, Update, DisplayItem, DisplayList, Distance };
        enum UpdateOptions { Exit, Assignment, PickedUp, Delivered, Charging, FinishCharging };
        enum ObjectMenu { Exit, Client, Drone, Station, Package };
        enum ObjectList { Exit, ClientList, DroneList, StationList, PackageList, PackageWithoutDrone, StationWithCharging };
        enum DistanceOptions { Exit, Client, Station };

        /// <summary>
        /// Main function to run the program, the program get user input and display the relevant application from user choice, User can: Add An object, Update different type of information, Display specific object and Display every element from different list.
        /// </summary>
        public static void Display(IDAL.IDAL dal)
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

                                    Console.WriteLine("Enter Station Data: Station ID, Station Name, Station location, Num of Vacant Stand  \n");  // Getting Station data from user
                                   


                                    break;

                                case ObjectMenu.Drone:

                                    Console.WriteLine("Enter Drone Data: ID, Model, Weight, Status, Battery \n");  // Getting Drone data from user    Dji_Mavic_2_Zoom, Dji_FPV_Combo
                                    
                                    break;

                                case ObjectMenu.Client:

                                    Console.WriteLine("Enter Client Data: ID, Name, Num of ChargingSlot, Longitude, Latitude\n");   // Getting Client data from user



                                  
                                    break;

                                case ObjectMenu.Package:
                                    Console.WriteLine("Enter All Package Data: SenderId, TargetId, DroneId, MaxWeight, Priority");  // Getting Package data from user
                                    



                                    break;

                                default:
                                    break;
                            }




                            break;
                        }

                    case Menu.Update:   //Update item
                        {
                           



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
            Console.WriteLine("Hello World!");
        }
    }
}
