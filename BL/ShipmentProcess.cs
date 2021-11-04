using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ShipmentProcess  //  משלוח בהעברה 
        {
            public int Id { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public ShipmentStatus Status { get; set; }
            public Location CollectingPosition { get; set; }
            public Location DestinationPosition { get; set; }
            public double DistanceShipping { get; set; }

            public override string ToString()
            {
                string result = "";
                result += $"ShipmentProcessId is {Id},\n";
                result += $"ShipmentProcessWeight is {Weight},\n";
                result += $"ShipmentProcessPriority is {Priority},\n";
                result += $"ShipmentProcessStat is {Status},\n";
                result += $"CollectingPosition is {CollectingPosition},\n";
                result += $"DestinationPosition is {DestinationPosition},\n";
                result += $"DistanceShipping is {DistanceShipping},\n";


                return result;
            }
        }
    }

}


