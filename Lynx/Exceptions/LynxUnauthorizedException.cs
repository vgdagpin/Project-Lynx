using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Lynx
{
    [Serializable]
    public class LynxUnauthorizedException : Exception
    {
        public LynxUnauthorizedException()
        {

        }

        public LynxUnauthorizedException(string message)
            : base(message)
        {

        }

        public LynxUnauthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public LynxUnauthorizedException(Exception exception)
            : base(exception.InnermostException().Message)
        {

        }

        protected LynxUnauthorizedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
