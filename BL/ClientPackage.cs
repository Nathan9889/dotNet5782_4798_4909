using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    namespace BO
    {
       public class ClientPackage //לקוח בחבילה  
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public override string ToString()
            {
                string result = "";
                result += $"ClientShipId is {ID},\n";
                result += $"ClientName is {Name},\n";
                
                return result;
            }
        }
    }

}
