using System;
using System.Runtime.Serialization;

namespace Lynx.Exceptions
{
    public class LynxNullException : Exception
    {
        public LynxNullException()
        {
        }

        public LynxNullException(string message) : base(message)
        {
        }

        public LynxNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LynxNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
