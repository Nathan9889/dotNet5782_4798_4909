using System;
using DalObject;


namespace ConsoleUI
{
    class Program
    {
        
        private static void Display()
        {
           
             int num = 1;

           

            while(num != 0)
            {
                Console.WriteLine("Choose an Option: \n");
                Console.WriteLine("1: Add \n 2: Update \n 3: Display Item: \n 4:Display List \n");
                int choice = int.Parse(Console.ReadLine());
                const int f = 0;

                switch (choice)
                {
                    
                    case 1:


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

                    default: Console.WriteLine("Invalid \n");
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
