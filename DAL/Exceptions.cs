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
            internal class IDException : Exception
            {
                int ID;
                public IDException()
                {
                }

                public IDException(string message) : base(message)
                {
                }

                public IDException(string message,int id) 
                {
                    ID = id;
                }

                public IDException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected IDException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

            
           

           

           


           
            
        }
    }
    
}
