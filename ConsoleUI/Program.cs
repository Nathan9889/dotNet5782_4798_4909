using System;


namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IDAL.DO.Client client = new IDAL.DO.Client
            {
                ID = 113,
                Name = "David",
                Latitude = 36.123456,
                Longitude = 29.654321
            };
            Console.WriteLine($"Name is {client.Name}, ID = {client.ID}");

           
        }
    }
}
