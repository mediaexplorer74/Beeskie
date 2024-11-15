using Bluesky.NET.Constants;
using Bluesky.NET.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bluesky.NET.ApiClients;

partial class BlueskyApiClient
{
    public async Task<Author?> GetAuthorAsync(string accessToken, string handle)
    {
        var timelineUrl = $"{UrlConstants.BlueskyBaseUrl}/{UrlConstants.ProfilePath}?actor={handle}";
        HttpRequestMessage message = new(HttpMethod.Get, timelineUrl);
        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        try
        {
            var httpResponse = await _httpClient.SendAsync(message);
            if (httpResponse.IsSuccessStatusCode)
            {
                using Stream contentStream = await httpResponse.Content.ReadAsStreamAsync();
                Author? response = JsonSerializer.Deserialize(contentStream, ModelSerializerContext.CaseInsensitive.Author);
                return response ?? null;
            }
        }
        catch (Exception e)
        {

        }

        return null;
    }

}
