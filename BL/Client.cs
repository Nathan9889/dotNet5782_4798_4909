﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL
{
    namespace BO
    {
        public class Client
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public Location ClientLocation { get; set; }
            public List<PackageAtClient> ClientsSender = new List<PackageAtClient>();
            public List<PackageAtClient> ClientsReceiver = new List<PackageAtClient>();

            public override string ToString()
            {
                string result = "";
                result += $"Name is {Name},\n";
                result += $"ID is {ID}, \n";
                result += $"Phone is {Phone.Substring(0, 3) + '-' + Phone.Substring(3)}, \n";
                result += $"Client Latitude is {ClientLocation.Latitude}, \n";
                result += $"Client Longitude is {ClientLocation.Longitude}, \n";
                if(ClientsSender.Count() > 0)
                {
                    result += $"ClientsSource is: \n";
                    foreach (var item in ClientsSender)
                    {
                        result += $"{item}\n";
                    }
                }

                if (ClientsReceiver.Count() > 0 )
                {
                    result += $"ClientsDestination is: \n";
                    foreach (var item in ClientsReceiver)
                    {
                        result += $"{item}\n";
                    }
                }

                return result;
            }
        }

    }
    


}