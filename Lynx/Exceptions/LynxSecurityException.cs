using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Lynx.Exceptions
{
    public class LynxSecurityException : Exception
    {
        private readonly string p_Provider;

        public LynxSecurityException()
        {

        }

        public LynxSecurityException(string provider, string message) 
            : base($"Provider: {provider} {Environment.NewLine}{message}")
        {
            
        }

        public LynxSecurityException(string message) : base(message)
        {
        }

        public LynxSecurityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LynxSecurityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
