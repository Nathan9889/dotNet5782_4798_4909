using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class ClientToList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public int DeliveredPackage { get; set; }
            public int UndeliveredPackage { get; set; }
            public int ReceivedPackage { get; set; }
            public int OnGoingPackage { get; set; }

            public override string ToString()
            {
                string result = "";
                result += $"Id is {Id},\n";
                result += $"Name is {Name},\n";
                result += $"Phone is {Phone},\n";
                result += $"num of DeliveredPackage is {DeliveredPackage},\n";
                result += $"UndeliveredPackage is {UndeliveredPackage},\n";
                result += $"DroneWithPackageCurrentLocation is {ReceivedPackage},\n";
                result += $"OnGoingPackage is {OnGoingPackage},\n";
                return result;
            }
        }

    }
    
}
