using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Lynx
{
    [Serializable]
    public class LynxException : Exception
    {
        public LynxException()
        {

        }

        public LynxException(string message)
            : base(message)
        {

        }

        public LynxException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public LynxException(Exception exception) 
            : base(exception.InnermostException().Message)
        {

        }

        protected LynxException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
