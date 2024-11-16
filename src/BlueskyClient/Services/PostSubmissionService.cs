using Bluesky.NET.ApiClients;
using Bluesky.NET.Constants;
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
    public event EventHandler? LikeSubmitted;

    public PostSubmissionService(
        IBlueskyApiClient blueskyApiClient,
        IUserSettings userSettings,
        IAuthenticationService authenticationService)
    {
        _blueskyApiClient = blueskyApiClient;
        _userSettings = userSettings;
        _authenticationService = authenticationService;
    }

    /// <inheritdoc/>
    public async Task<bool> LikeAsync(string targetUri, string targetCid)
    {
        if (string.IsNullOrEmpty(targetUri) || string.IsNullOrEmpty(targetCid))
        {
            return false;
        }

        var token = await _authenticationService.TryGetFreshTokenAsync();
        var handle = _userSettings.Get<string>(UserSettingsConstants.LastUsedUserHandleKey);

        if (token is null || handle is null)
        {
            return false;
        }


        FeedRecord newRecord = new()
        {
            CreatedAt = DateTime.Now,
            Subject = new RecordSubject
            {
                Uri = targetUri,
                Cid = targetCid
            }
        };

        CreateRecordResponse? result = await _blueskyApiClient.SubmitPostAsync(token, handle, newRecord, RecordType.Like);
        if (result is not null)
        {
            LikeSubmitted?.Invoke(this, EventArgs.Empty);
        }

        return result is not null;
    }

    /// <inheritdoc/>
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

        CreateRecordResponse? result = await _blueskyApiClient.SubmitPostAsync(token, handle, newRecord, RecordType.NewPost);
        if (result is not null)
        {
            NewPostSubmitted?.Invoke(this, EventArgs.Empty);
        }

        return result?.Uri;
    }
}
