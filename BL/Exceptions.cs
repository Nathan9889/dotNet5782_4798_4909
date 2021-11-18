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

            [Serializable]
            public class PackageIdException : Exception
            {
                private string v;
                private int iD;

                public PackageIdException()
                {
                }

                public PackageIdException(string message) : base(message)
                {
                }

                public PackageIdException(string v, int iD)
                {
                    this.v = v;
                    this.iD = iD;
                }

                public PackageIdException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected PackageIdException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }

                public override string ToString()
                {
                    return Message + $"ID: {iD}";
                }
            }



            [Serializable]
            public class BLPackageException : Exception
            {
                public BLPackageException()
                {
                }

                public BLPackageException(string message) : base(message)
                {
                }

                public BLPackageException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected BLPackageException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            public class BLStationException : Exception
            {
                public BLStationException()
                {
                }

                public BLStationException(string message) : base(message)
                {
                }

                public BLStationException(string message, Exception innerException) : base(message, innerException)
                {
                }

                

                protected BLStationException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class BLClientException : Exception
            {
                public BLClientException()
                {
                }

                public BLClientException(string message) : base(message)
                {
                }

                public BLClientException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected BLClientException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
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
                private string v;
                private int iD;

                public IdNotFoundException()
                {
                }

                public IdNotFoundException(string message) : base(message)
                {
                }

                public IdNotFoundException(string v, int iD)
                {
                    this.v = v;
                    this.iD = iD;
                }

                public IdNotFoundException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected IdNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class NotFound : Exception
            {
                private string v;
                private int id;

                public NotFound()
                {
                }

                public NotFound(string message) : base(message)
                {
                }

                public NotFound(string v, int id)
                {
                    this.v = v;
                    this.id = id;
                }

                public NotFound(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected NotFound(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class DroneTaken : Exception
            {
                public DroneTaken()
                {
                }

                public DroneTaken(string message) : base(message)
                {
                }

                public DroneTaken(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected DroneTaken(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class LocationOutOfRange : Exception
            {
                private string v;
                private int iD;

                public LocationOutOfRange()
                {
                }

                public LocationOutOfRange(string message) : base(message)
                {
                }

                public LocationOutOfRange(string v, int iD)
                {
                    this.v = v;
                    this.iD = iD;
                }

                public LocationOutOfRange(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected LocationOutOfRange(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
        }
    }

}
