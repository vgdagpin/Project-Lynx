using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Lynx.Interfaces;

namespace Lynx.MobileApp.Portable.Common
{
    public class AppJsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data);
        }

        public string Serialize<T>(T data)
        {
            var _serializerSettings = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            };

            return JsonSerializer.Serialize(data, _serializerSettings);
        }
    }
}