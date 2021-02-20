using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Lynx.MobileApp
{
    public static class IHttpClientFactoryExtensions
    {
        public static HttpClient LynxApiClient(this IHttpClientFactory factory)
        {
            return factory.CreateClient("lynx-api");
        }
    }
}
