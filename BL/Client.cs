using System;
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
            List<PackageAtClient> ClientsSender;
            List<PackageAtClient> ClientsReceiver;

            public override string ToString()
            {
                string result = "";
                result += $"Name is {Name},\n";
                result += $"ID is {ID}, \n";
                result += $"Phone is {Phone.Substring(0, 3) + '-' + Phone.Substring(3)}, \n";
                result += $"ClientLocation is {ClientLocation}, \n";
                //result += $"ClientsSource is {ClientsSender}, \n";
                //result += $"ClientsDestination is {ClientsReceiver}, \n";

                return result;
            }
        }

    }
    


}