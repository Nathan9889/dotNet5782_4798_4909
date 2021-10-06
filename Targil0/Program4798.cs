using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome4798();
            Welcome4909();
            Console.ReadKey();
        }

        static partial void Welcome4909();

        private static void Welcome4798()
        {
            Console.WriteLine("Enter your name: ");
            string userName;
            userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
    }
}
