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
       public class ClientShip //לקוח במשלוח 
        {
            public string Name { get; set; }
            public int Id { get; set; }

            public override string ToString()
            {
                string result = "";
                result += $"ClientShipId is {Id},\n";
                result += $"ClientName is {Name},\n";
                
                return result;
            }
        }
    }

}
