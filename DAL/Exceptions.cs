using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        class Exceptions : Exception
        {
            [Serializable]
            internal class ThereIsNoIDClient : Exception
            {
                public ThereIsNoIDClient()
                {
                }

                public ThereIsNoIDClient(string message) : base(message)
                {
                }

                public ThereIsNoIDClient(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected ThereIsNoIDClient(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class ThereIsNoIDPackage : Exception
            {
                public ThereIsNoIDPackage()
                {
                }

                public ThereIsNoIDPackage(string message) : base(message)
                {
                }

                public ThereIsNoIDPackage(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected ThereIsNoIDPackage(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class ThereIsNoIDDrone : Exception
            {
                public ThereIsNoIDDrone()
                {
                }

                public ThereIsNoIDDrone(string message) : base(message)
                {
                }

                public ThereIsNoIDDrone(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected ThereIsNoIDDrone(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class ThereIsNoIDStation : Exception
            {
                public ThereIsNoIDStation()
                {
                }

                public ThereIsNoIDStation(string message) : base(message)
                {
                }

                public ThereIsNoIDStation(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected ThereIsNoIDStation(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class ThereIsNoIDDroneCharge : Exception
            {
                public ThereIsNoIDDroneCharge()
                {
                }

                public ThereIsNoIDDroneCharge(string message) : base(message)
                {

                }

                public ThereIsNoIDDroneCharge(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected ThereIsNoIDDroneCharge(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class StationIDAlreadyExists : Exception
            {
                public StationIDAlreadyExists()
                {
                }

                public StationIDAlreadyExists(string message) : base(message)
                {
                }

                public StationIDAlreadyExists(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected StationIDAlreadyExists(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class DroneIDAlreadyExists : Exception
            {
                public DroneIDAlreadyExists()
                {
                }

                public DroneIDAlreadyExists(string message) : base(message)
                {
                }

                public DroneIDAlreadyExists(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected DroneIDAlreadyExists(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class ClientIDAlreadyExists : Exception
            {
                public ClientIDAlreadyExists()
                {
                }

                public ClientIDAlreadyExists(string message) : base(message)
                {
                }

                public ClientIDAlreadyExists(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected ClientIDAlreadyExists(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
        }
    }
    
}
