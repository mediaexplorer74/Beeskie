using Bluesky.NET.ApiClients;
using Bluesky.NET.Constants;
using Bluesky.NET.Models;
using BlueskyClient.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueskyClient.Services;

public class NotificationsService : INotificationsService
{
    private readonly IBlueskyApiClient _blueskyApiClient;
    private readonly IAuthenticationService _authenticationService;

    public NotificationsService(
        IBlueskyApiClient blueskyApiClient,
        IAuthenticationService authenticationService)
    {
        _blueskyApiClient = blueskyApiClient;
        _authenticationService = authenticationService;
    }

    public async Task<IReadOnlyList<Notification>> GetNotificationsAsync()
    {
        var token = await _authenticationService.TryGetFreshTokenAsync();
        if (token is null)
        {
            return [];
        }

        return await _blueskyApiClient.GetNotificationsAsync(token);
    }

    public async Task HydrateAsync(NotificationViewModel notification)
    {
        var token = await _authenticationService.TryGetFreshTokenAsync();
        if (token is null)
        {
            return;
        }

        if (notification.Reason is ReasonConstants.Like or ReasonConstants.Repost &&
            notification.Notification.ReasonSubject is string { Length: > 0 } subjectUri)
        {
            var subjectPosts = await _blueskyApiClient.GetPostsAsync(token, [subjectUri]);
            notification.SubjectPost = subjectPosts.Count > 0
                ? subjectPosts[0]
                : null;
        }
    }
}
