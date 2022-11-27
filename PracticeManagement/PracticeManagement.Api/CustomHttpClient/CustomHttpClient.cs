using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;

namespace PracticeManagement.Api.CustomHttpClient
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
            return _client.PostAsJsonAsync<TValue>(requestUri, value, options, cancellationToken);
        }
    }
}
