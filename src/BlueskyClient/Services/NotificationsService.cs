using Bluesky.NET.ApiClients;
using Bluesky.NET.Models;
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
}
