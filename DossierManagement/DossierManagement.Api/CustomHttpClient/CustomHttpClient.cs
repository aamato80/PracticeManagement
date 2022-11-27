using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;

namespace DossierManagement.Api.CustomHttpClient
{
    public class CustomHttpClient : ICustomHttpClient
    {
        private readonly HttpClient _client;

        public CustomHttpClient(IHttpClientFactory httpClientFactory)
        { 
            _client = httpClientFactory.CreateClient();
        }

        public Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string? requestUri, TValue value, JsonSerializerOptions? options, CancellationToken cancellationToken)
        {
            return _client.PostAsJsonAsync(requestUri, value, options, cancellationToken);
        }
    }
}
