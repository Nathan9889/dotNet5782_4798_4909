using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {

        /// <summary>
        /// ... ID can not be negative
        /// A ... ID already exists
        /// </summary>
        public class Exceptions : Exception
        {
            [Serializable]
            public class IDException : Exception
            {
               
                public int iD;

                public IDException()
                {
                }

                public IDException(string message) : base(message)
                {
                }

                public IDException(string v, int iD) : base(v)
                {
                    
                    this.iD = iD;
                }

                public IDException(string message, Exception innerException, int id) : base(message, innerException)
                {
                    this.iD = id;
                }

                protected IDException(SerializationInfo info, StreamingContext context) : base(info, context)
                {

                }

                public override string ToString()
                {
                    return Message + $"ID: {iD}";
                }
            }

            /// <summary>
            /// There are no charging slots available at the station
            /// There are no charging slots available at any station
            /// </summary>
            [Serializable]
            public class StationException : Exception
            {
                
                public int stationNumToCharge = 0;

                public StationException()
                {
                }

                public StationException(string message) : base(message)
                {
                }

                public StationException(string v, int stationNumToCharge)
                {
                    
                    this.stationNumToCharge = stationNumToCharge;
                }

                public StationException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected StationException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }

                public override string ToString()
                {
                    if (stationNumToCharge == 0) return Message;
                    return Message + $"stationNumToCharge: {stationNumToCharge}";
                }

            }




            [Serializable]
            internal class SendingDroneToCharging : Exception
            {
                public SendingDroneToCharging()
                {
                }

                public SendingDroneToCharging(string message) : base(message)
                {
                }

                public SendingDroneToCharging(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected SendingDroneToCharging(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }

                public override string ToString()
                {

                    return Message;
                }
            }
        }
    }

}
