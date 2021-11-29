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
                    return Message + $" ID: {iD}";
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

                public StationException(string v, int stationNumToCharge) : base(v)
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

            /// <summary>
            /// battery/charging reference exceptions
            /// </summary>
            [Serializable]
            public class SendingDroneToCharging : Exception
            {
                int ID;
                public SendingDroneToCharging()
                {
                }

                public SendingDroneToCharging(string message, int iD) : base(message)
                {
                    this.ID = iD;
                }

                public SendingDroneToCharging(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected SendingDroneToCharging(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }

                public override string ToString()
                {

                    return Message + $"id: {ID}";
                }
            }

            /// <summary>
            /// The status of the drone is charging but it is not in the droneCharges list
            /// </summary>
            [Serializable]
            internal class EndDroneCharging : Exception
            {
                
                private int droneID;

                public EndDroneCharging()
                {
                }

                public EndDroneCharging(string message) : base(message)
                {
                }

                public EndDroneCharging(string v, int droneID):base(v)
                {
                    
                    this.droneID = droneID;
                }

                public EndDroneCharging(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected EndDroneCharging(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }

                public override string ToString()
                {
                    return Message + $"ID: {droneID}";
                }
            }

            [Serializable]
            internal class IdNotFoundException : Exception
            {
                
                private int iD;

                public IdNotFoundException()
                {
                }

                public IdNotFoundException(string message) : base(message)
                {
                }

                public IdNotFoundException(string v, int iD): base(v)
                {
                    
                    this.iD = iD;
                }

                public IdNotFoundException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected IdNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
                public override string ToString()
                {
                    return Message + $" ID: {iD}";
                }
            }

           

            [Serializable]
            internal class LocationOutOfRange : Exception
            {
                
                private int iD;

                public LocationOutOfRange()
                {
                }

                public LocationOutOfRange(string message) : base(message)
                {
                }

                public LocationOutOfRange(string v, int iD) : base(v)
                {
                    
                    this.iD = iD;
                }

                public LocationOutOfRange(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected LocationOutOfRange(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }

                public override string ToString()
                {
                    return Message + $" ID: {iD}";
                }
            }

            [Serializable]
            internal class UnablePickedUpPackage : Exception
            {
                
                private int iD;

                public UnablePickedUpPackage()
                {
                }

                public UnablePickedUpPackage(string message) : base(message)
                {
                }

                public UnablePickedUpPackage(string v, int iD) : base(v)
                {
                   
                    this.iD = iD;
                }

                public UnablePickedUpPackage(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected UnablePickedUpPackage(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
                public override string ToString()
                {
                    return Message + $" ID: {iD}";
                }
            }

            [Serializable]
            internal class UnableAssociatPackage : Exception
            {
                public UnableAssociatPackage()
                {
                }

                public UnableAssociatPackage(string message) : base(message)
                {
                }

                public UnableAssociatPackage(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected UnableAssociatPackage(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
                public override string ToString()
                {
                    return Message;
                }
            }

            [Serializable]
            internal class PhoneExceptional : Exception
            {
               
                private string phone;

                public PhoneExceptional()
                {
                }

                public PhoneExceptional(string message) : base(message)
                {
                }

                public PhoneExceptional(string v, string phone) : base(v)
                {
                    
                    this.phone = phone;
                }

                public PhoneExceptional(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected PhoneExceptional(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
                public override string ToString()
                {
                    return Message + $"Phone: {phone}";
                }
            }

            [Serializable]
            internal class NegativeException : Exception
            {
                
                private int ID;

                public NegativeException()
                {
                }

                public NegativeException(string message) : base(message)
                {
                }

                public NegativeException(string v, int ID) : base(v)
                {
                    this.ID = ID;
                }

                public NegativeException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected NegativeException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
                public override string ToString()
                {
                    return Message + $": ID is {ID}";
                }
            }
        }
    }

}
