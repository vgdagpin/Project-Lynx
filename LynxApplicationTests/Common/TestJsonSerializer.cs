using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LynxApplicationTests.Common
{
    public class TestJsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public string Serialize<T>(T data)
        {
            var _serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            _serializerSettings.Converters.Add(new StringEnumConverter());

            return JsonConvert.SerializeObject(data, _serializerSettings);
        }
    }
}
