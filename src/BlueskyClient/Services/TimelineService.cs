using Bluesky.NET.ApiClients;
using Bluesky.NET.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueskyClient.Services;

public class TimelineService : ITimelineService
{
    private readonly IBlueskyApiClient _apiClient;
    private readonly IAuthenticationService _authentication;

    public TimelineService(
        IBlueskyApiClient blueskyApiClient,
        IAuthenticationService authenticationService)
    {
        _apiClient = blueskyApiClient;
        _authentication = authenticationService;
    }

    public async Task<IReadOnlyList<FeedItem>> GetTimelineAsync()
    {
        if (await _authentication.TryGetFreshTokenAsync() is string { Length: > 0 } token)
        {
            return await _apiClient.GetTimelineAsync(token);
        }

        return [];
    }
}
