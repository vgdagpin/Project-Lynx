using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Lynx.Interfaces;
using Newtonsoft.Json;

namespace Lynx.MobileApp.Portable.Common
{
    public class AppJsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string data)
        {
            //var result = JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions
            //{
            //    Converters =
            //    {
            //        new JsonStringEnumConverter()
            //    }
            //});
            var result = JsonConvert.DeserializeObject<T>(data);

            return result;
        }

        public string Serialize<T>(T data)
        {
            //var _serializerSettings = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = null,
            //    Converters =
            //    {
            //        new JsonStringEnumConverter()
            //    }
            //};

            var result = JsonConvert.SerializeObject(data);

            return result;
        }
    }
}