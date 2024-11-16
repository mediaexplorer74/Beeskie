using Bluesky.NET.Constants;
using Bluesky.NET.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bluesky.NET.ApiClients;

partial class BlueskyApiClient
{
    public async Task<IReadOnlyList<FeedItem>> GetTimelineAsync(string accesstoken)
    {
        var timelineUrl = $"{UrlConstants.BlueskyBaseUrl}/{UrlConstants.TimelinePath}";
        HttpRequestMessage message = new(HttpMethod.Get, timelineUrl);
        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
        
        try
        {
            var httpResponse = await _httpClient.SendAsync(message);
            if (httpResponse.IsSuccessStatusCode)
            {
                using Stream contentStream = await httpResponse.Content.ReadAsStreamAsync();
                FeedResponse? response = JsonSerializer.Deserialize(contentStream, ModelSerializerContext.CaseInsensitive.FeedResponse);
                return response?.Feed ?? [];
            }
        }
        catch (Exception e)
        {

        }

        return [];
    }
}
