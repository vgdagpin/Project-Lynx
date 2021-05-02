using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Domain.Models;
using Lynx.Interfaces;

namespace Lynx.Common
{
    public class LynxAPI : ILynxAPI
    {
        private readonly HttpClient p_HttpClient;
        private readonly IJsonSerializer p_JsonSerializer;

        public LynxAPI(HttpClient httpClient, IJsonSerializer jsonSerializer)
        {
            p_HttpClient = httpClient;
            p_JsonSerializer = jsonSerializer;
        }

        public async Task<LynxResponse<T>> GetAsync<T>(string requestUri, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var httpResponse = await p_HttpClient.SendAsync(request, cancellationToken);

            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                return default;
            }

            if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new LynxUnauthorizedException();
            }

            return new LynxResponse<T>
            {
                ObjectContent = p_JsonSerializer.Deserialize<T>(jsonContent),
                StringContent = jsonContent,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                StatusCode = httpResponse.StatusCode
            };
        }

        public async Task<LynxResponse<T>> PostAsync<T, TBody>(string requestUri, TBody body, CancellationToken cancellationToken = default)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new StringContent(p_JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            };

            var httpResponse = await p_HttpClient.SendAsync(httpRequest, cancellationToken);

            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new LynxUnauthorizedException();
            }

            if (!httpResponse.IsSuccessStatusCode)
            {
                return default;
            }

            return new LynxResponse<T>
            {
                ObjectContent = p_JsonSerializer.Deserialize<T>(jsonContent),
                StringContent = jsonContent,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                StatusCode = httpResponse.StatusCode
            };
        }
    }
}
