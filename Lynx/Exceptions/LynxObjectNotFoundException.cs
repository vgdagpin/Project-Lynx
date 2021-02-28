using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Lynx
{
    [Serializable]
    public class LynxObjectNotFoundException<TType> : Exception where TType : class
    {
        public LynxObjectNotFoundException()
            : base($"Object {typeof(TType).Name} not found")
        {

        }

        public LynxObjectNotFoundException(object objectKey)
            : base($"Object {typeof(TType).Name} not found for objectKey: {ParseObjectKey(objectKey)}")
        {

        }

        public LynxObjectNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected LynxObjectNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string ParseObjectKey(object objectKey)
        {
            Type t = objectKey.GetType();

            if (!t.IsClass)
            {
                return objectKey.ToString();
            }

            Dictionary<string, string> props = new Dictionary<string, string>();

            t.GetProperties()
               .ToList()
               .ForEach(a =>
               {
                   props[a.Name] = a.GetValue(objectKey)?.ToString();
               });

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("{");

            sb.AppendLine(string.Join(",", props.Select(a => $"\"{a.Key}\" : \"{a.Value}\"")));

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
