using Bluesky.NET.ApiClients;
using Bluesky.NET.Models;
using BlueskyClient.Constants;
using JeniusApps.Common.Settings;
using JeniusApps.Common.Telemetry;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueskyClient.Services;

public class TimelineService : ITimelineService
{
    private readonly IBlueskyApiClient _apiClient;
    private readonly IAuthenticationService _authentication;
    private readonly IProfileService _profileService;
    private readonly ITelemetry _telemetry;
    private readonly Lazy<Task<Author?>> _currentUser;

    public TimelineService(
        IBlueskyApiClient blueskyApiClient,
        IAuthenticationService authenticationService,
        IProfileService profileService,
        ITelemetry telemetry)
    {
        _apiClient = blueskyApiClient;
        _authentication = authenticationService;
        _profileService = profileService;
        _telemetry = telemetry;

        _currentUser = new Lazy<Task<Author?>>(() => _profileService.GetCurrentUserAsync());
    }

    public async Task<IReadOnlyList<FeedItem>> GetTimelineAsync()
    {
        var author = await _currentUser.Value;
        if (author?.Handle is not { Length: > 0 } currentUserHandle)
        {
            return [];
        }

        if (await _authentication.TryGetFreshTokenAsync() is string { Length: > 0 } token)
        {
            IReadOnlyList<FeedItem> rawTimeline;
            try
            {
                rawTimeline = await _apiClient.GetTimelineAsync(token);
            }
            catch (Exception e)
            {
                var dict = new Dictionary<string, string>
                {
                    { "method", "GetTimelineAsync" },
                    { "message", e.Message },
                };
                _telemetry.TrackError(e, dict);
                _telemetry.TrackEvent(TelemetryConstants.ApiError, dict);

                rawTimeline = [];
            }

            List<FeedItem> results = [];
            foreach (var t in rawTimeline)
            {
                if (t.Reply is { Parent.Author: Author parentAuthor } &&
                    parentAuthor.Handle != currentUserHandle)
                {
                    // Filter out replies for now if they aren't responding to the current user.
                    // In the future, need to properly build this experience. 
                    continue;
                }

                results.Add(t);
            }

            return results;
        }

        return [];
    }
}
