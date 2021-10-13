using System;


namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            myClient();
            //Console.WriteLine($"Name is {client.Name}, ID = {client.ID}");


        }

        private static void myClient()
        {
            IDAL.DO.Client client = new IDAL.DO.Client
            {
                ID = 113,
                Name = "David",
                Latitude = 36.123456,
                Longitude = 29.654321,
                Phone = "0526137053"
            };
            Console.WriteLine(client);
        }
    }
}
