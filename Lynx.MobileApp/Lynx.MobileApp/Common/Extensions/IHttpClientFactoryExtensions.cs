using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Lynx.Domain.Entities;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;

namespace Lynx.MobileApp
{
    public static class IHttpClientFactoryExtensions
    {
        public static HttpClient LynxApiClient(this IHttpClientFactory factory, UserSessionBO userSession = null)
        {
            HttpClient client = factory.CreateClient("lynx-api");

            if (userSession != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userSession.Token);
            }

            return client;
        }
    }
}
