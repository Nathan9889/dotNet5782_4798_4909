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
            // רשימת משלוחים אצל לקוח - מהלקוח
            // רשימת משלוחים אצל לקוח - אל הלקוח 


            public override string ToString()
            {
                string result = "";
                result += $"Name is {Name},\n";
                result += $"ID is {ID}, \n";
                result += $"Phone is {Phone.Substring(0, 3) + '-' + Phone.Substring(3)}, \n";
                result += $"ClientLocation is {ClientLocation}, \n";

                //
                //

                return result;
            }
        }

    }
    


}