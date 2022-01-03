using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


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

            public IDException(string v, int iD) : base(v)
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

        [Serializable]
        internal class IDalNotFound : Exception
        {
            private string v;
            private string type;

            public IDalNotFound()
            {
            }

            public IDalNotFound(string message) : base(message)
            {

            }

            public IDalNotFound(string v, string type) : base(v)
            {
                this.v = v;
                this.type = type;
            }

            public IDalNotFound(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected IDalNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        [Serializable]
        internal class XMLFileLoadCreateException : Exception
        {
            private string filePath;
            private string v;
            private Exception ex;

            public XMLFileLoadCreateException()
            {
            }

            public XMLFileLoadCreateException(string message) : base(message)
            {
            }

            public XMLFileLoadCreateException(string message, Exception innerException) : base(message, innerException)
            {
            }

            public XMLFileLoadCreateException(string filePath, string v, Exception ex)
            {
                this.filePath = filePath;
                this.v = v;
                this.ex = ex;
            }

            protected XMLFileLoadCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}


