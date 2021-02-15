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

        protected LynxException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
