using Bluesky.NET.Constants;
using Bluesky.NET.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bluesky.NET.ApiClients;

public class BlueskyApiClient : IBlueskyApiClient
{
    private readonly HttpClient _httpClient = new();

    public async Task<AuthResponse?> RefreshAsync(string refreshToken)
    {
        var refreshUrl = $"{UrlConstants.BlueskyBaseUrl}/{UrlConstants.RefreshAuthPath}";
        HttpRequestMessage message = new(HttpMethod.Post, refreshUrl);
        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", refreshToken);
        return await PostAuthMessageAsync(message);
    }

    /// <inheritdoc/>
    public async Task<AuthResponse?> AuthenticateAsync(string userHandle, string appPassword)
    {
        var authUrl = $"{UrlConstants.BlueskyBaseUrl}/{UrlConstants.AuthPath}";

        var requestBody = new AuthRequestBody
        {
            Identifier = userHandle,
            Password = appPassword
        };

        HttpRequestMessage message = new(HttpMethod.Post, authUrl)
        {
            Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
        };

        return await PostAuthMessageAsync(message);
    }

    private async Task<AuthResponse?> PostAuthMessageAsync(HttpRequestMessage message)
    {
        try
        {
            var response = await _httpClient.SendAsync(message);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new AuthResponse
                {
                    Success = false,
                    ErrorMessage = errorContent
                };
            }

            using Stream resultStream = await response.Content.ReadAsStreamAsync();
            var authResponse = await JsonSerializer.DeserializeAsync(
                resultStream,
                ModelSerializerContext.CaseInsensitive.AuthResponse);

            if (authResponse is not null)
            {
                authResponse.Success = true;
                return authResponse;
            }
        }
        catch (Exception e)
        {
            return new AuthResponse
            {
                Success = false,
                ErrorMessage = e.Message
            };
        }

        return null;
    }
}
