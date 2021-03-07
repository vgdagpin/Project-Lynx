using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Lynx.MobileApp
{
    public class JsonContent<T> : StringContent where T : class
    {
        public JsonContent(T data) : base(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
        {
        }
    }
}
