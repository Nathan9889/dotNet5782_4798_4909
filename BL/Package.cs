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
        public class Package
        {
            public int ID { get; set; }
            public ClientPackage SenderClient { get; set; }
            public ClientPackage TargetClient { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DroneOfPackage DroneOfPackage { get; set; }
            public DateTime? Created { get; set; }
            public DateTime? Associated { get; set; }
            public DateTime? PickedUp { get; set; }
            public DateTime? Delivered { get; set; }

            public override string ToString()
            {
                string result = "";
                result += $"Package ID is {ID}, \n";
                result += $"Package SenderClient info :\n {SenderClient}\n";
                result += $"Package ReceiverClient info :\n {TargetClient} \n";
                result += $"Package Weight is {Weight},\n";
                result += $"Package Priority is {Priority},\n";
                if(DroneOfPackage != null) result += $"Drone Of Package info:\n {DroneOfPackage} \n"; // רק אם החבילה שויכה אתחלנו את השדה הזה
                result += $"Package Created at {Created},\n";
                if(Associated != DateTime.MinValue) result += $"Package Associated at {Associated},\n";
                if (PickedUp != DateTime.MinValue) result += $"Package PickedUp at {PickedUp}, \n";
                if (Delivered != DateTime.MinValue) result += $"Package Delivered at {Delivered}, \n";

                return result;
            }
        }
    }
}