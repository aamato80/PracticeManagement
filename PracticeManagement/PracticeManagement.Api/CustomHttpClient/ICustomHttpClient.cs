﻿using System.Text.Json;

namespace PracticeManagement.Api.CustomHttpClient
{
    public interface ICustomHttpClient
    {
        Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default);
    }
}
