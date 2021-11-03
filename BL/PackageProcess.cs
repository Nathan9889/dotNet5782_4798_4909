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
        public class PackagProcess  //חבילה בהעברה 
        {
            public int Id { get; set; }
            public Priorities Priority { get; set; }


            //      לקוח במשלוח
            //      מקבל

            public override string ToString()
            {
                string result = "";
                result += $"PackagProcessId is {Id},\n";
                result += $"PackagProcessPriority is {Priority},\n";
                //result += $"////    is {     },\n";
                //result += $"////    is {     },\n";

                return result;
            }
        }
    }

}
