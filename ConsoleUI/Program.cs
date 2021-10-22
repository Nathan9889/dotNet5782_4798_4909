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
                                int clientId = Convert.ToInt32(Console.ReadLine());
                                string clientName = Console.ReadLine();
                                string clientPhone = Console.ReadLine();
                                double clientLatitude = Convert.ToInt32(Console.ReadLine());
                                double clientLongitude = Convert.ToInt32(Console.ReadLine());

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



                                break;
                            case ObjectMenu.Station:


                                break;
                            case ObjectMenu.Package:


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
