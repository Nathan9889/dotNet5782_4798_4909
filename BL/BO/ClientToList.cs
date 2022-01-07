using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ClientToList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int sentAndDeliveredPackage { get; set; }
        public int sentAndUndeliveredPackage { get; set; }
        public int ReceivedAndDeliveredPackage { get; set; }
        public int ReceivedAndUnDeliveredPackage { get; set; }

        public override string ToString()
        {
            string result = "";
            result += $"Client Id is {Id},\n";
            result += $"Client Name is {Name},\n";
            result += $"Client Phone is {Phone.Substring(0, 3) + '-' + Phone.Substring(3)}, \n";
            result += $"Number of sent and Delivered Package is {sentAndDeliveredPackage},\n";
            result += $"Number of sent but Undelivered Package is {sentAndUndeliveredPackage},\n";
            result += $"Number of received and Delivered Package is {ReceivedAndDeliveredPackage},\n";
            result += $"Number of received but UnDelivered Package is {ReceivedAndUnDeliveredPackage}.\n";
            return result;
        }
    }
}


