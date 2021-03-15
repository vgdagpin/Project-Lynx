using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;

namespace Lynx
{
    [Serializable]
    public class LynxHttpException : Exception
    {
        public LynxHttpException(HttpResponseMessage response) : base(ParseResponseMessage(response))
        {

        }

        private static string ParseResponseMessage(HttpResponseMessage response)
        {
            string jsonContent = response.Content.ReadAsStringAsync().Result;

            if (!string.IsNullOrWhiteSpace(jsonContent))
            {
                return $"{response.StatusCode} - {jsonContent}";
            }

            return response.StatusCode.ToString();
        }

        protected LynxHttpException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
