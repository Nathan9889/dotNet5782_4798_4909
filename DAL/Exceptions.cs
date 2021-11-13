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
        public class Exceptions : Exception

        { /// <summary>
          /// A ... ID already exists
          /// ... ID not found
          /// </summary>
          /// 
            
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

                public IDException(string v, int iD):base(v)
                {
                    
                    this.iD = iD;
                }

                public IDException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected IDException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }

                public override string ToString()
                {
                    return Message + "ID: {iD}";
                }
            }

            
            
        }
    }
    
}
