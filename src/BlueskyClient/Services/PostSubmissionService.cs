using Bluesky.NET.ApiClients;
using Bluesky.NET.Models;
using BlueskyClient.Constants;
using JeniusApps.Common.Settings;
using System;
using System.Threading.Tasks;

namespace BlueskyClient.Services;

public class PostSubmissionService : IPostSubmissionService
{
    private readonly IBlueskyApiClient _blueskyApiClient;
    private readonly IUserSettings _userSettings;
    private readonly IAuthenticationService _authenticationService;

    public event EventHandler? NewPostSubmitted;

    public PostSubmissionService(
        IBlueskyApiClient blueskyApiClient,
        IUserSettings userSettings,
        IAuthenticationService authenticationService)
    {
        _blueskyApiClient = blueskyApiClient;
        _userSettings = userSettings;
        _authenticationService = authenticationService;
    }

    public async Task<string?> SubmitPostAsync(string text)
    {
        text = text.Trim();
        if (string.IsNullOrEmpty(text))
        {
            return null;
        }

        var token = await _authenticationService.TryGetFreshTokenAsync();
        var handle = _userSettings.Get<string>(UserSettingsConstants.LastUsedUserHandleKey);

        if (token is null || handle is null)
        {
            return null;
        }


        FeedRecord newRecord = new()
        {
            CreatedAt = DateTime.Now,
            Text = text
        };

        CreateRecordResponse? result = await _blueskyApiClient.SubmitPostAsync(token, handle, newRecord);
        if (result is not null)
        {
            NewPostSubmitted?.Invoke(this, EventArgs.Empty);
        }

        return result?.Uri;
    }
}
