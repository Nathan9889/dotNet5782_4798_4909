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
        public class ShipToClient  //משלוח אצל לקוח 
        {
            public int Id { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }

            public ShipmentStatus ShipmentStat { get; set; }

            //      לקוח במשלוח   

            public override string ToString()
            {
                string result = "";
                result += $"ShipToClientId is {Id},\n";
                result += $"ShipToClientWeight is {Weight},\n";
                result += $"ShipToClientPriority is {Priority},\n";
                result += $"ShipToClientStatus is {ShipmentStat},\n";
                //result += $"////    is {     },\n";
                return result;
            }
        }
    }

}
