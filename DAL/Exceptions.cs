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
            internal class ClientException : Exception
            {
                public ClientException()
                {
                }

                public ClientException(string message) : base(message)
                {
                }

                public ClientException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected ClientException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class PackageException : Exception
            {
                public PackageException()
                {
                }

                public PackageException(string message) : base(message)
                {
                }

                public PackageException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected PackageException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class DroneException : Exception
            {
                public DroneException()
                {
                }

                public DroneException(string message) : base(message)
                {
                }

                public DroneException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected DroneException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class StationException : Exception
            {
                public StationException()
                {
                }

                public StationException(string message) : base(message)
                {
                }

                public StationException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected StationException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class DroneChargeException : Exception
            {
                public DroneChargeException()
                {
                }

                public DroneChargeException(string message) : base(message)
                {

                }

                public DroneChargeException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected DroneChargeException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class ExistingStationId : Exception
            {
                public ExistingStationId()
                {
                }

                public ExistingStationId(string message) : base(message)
                {
                }

                public ExistingStationId(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected ExistingStationId(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class ExistingDroneId : Exception
            {
                public ExistingDroneId()
                {
                }

                public ExistingDroneId(string message) : base(message)
                {
                }

                public ExistingDroneId(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected ExistingDroneId(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            [Serializable]
            internal class ExistingClientId : Exception
            {
                public ExistingClientId()
                {
                }

                public ExistingClientId(string message) : base(message)
                {
                }

                public ExistingClientId(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected ExistingClientId(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
        }
    }
    
}
