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
                result += $"Client of the Package Id is {ID},\n";
                result += $"Client of the Package Name is {Name}.\n";
                
                return result;
            }
        }
    }

}
