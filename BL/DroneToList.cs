using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
   namespace BO
    {
        public class DroneToList
        {
            public int ID { get; set; }
            public DroneModel Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public double Battery { get; set; }
            public DroneStatus Status { get; set; }
            public Location DroneLocation { get; set; }
            public int PackageID { get; set; }

        }
    }
}
