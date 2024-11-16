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

    public event EventHandler<(SubmissionRecord, CreateRecordResponse)>? RecordCreated;

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
    public async Task<string?> ReplyAsync(string text, FeedPost parent)
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

        var root = parent.Record.Reply?.Root is RecordSubject existingRoot
            ? existingRoot
            : new RecordSubject
            {
                Cid = parent.Cid,
                Uri = parent.Uri
            };


        SubmissionRecord newRecord = new()
        {
            CreatedAt = DateTime.Now,
            Text = text,
            Reply = new ReplyRecord
            {
                Parent = new RecordSubject()
                {
                    Cid = parent.Cid,
                    Uri = parent.Uri
                },
                Root = root
            }
        };

        CreateRecordResponse? result = await _blueskyApiClient.SubmitPostAsync(token, handle, newRecord, RecordType.Reply);
        if (result is not null)
        {
            RecordCreated?.Invoke(this, (newRecord, result));
        }

        return result?.Uri;
    }

    /// <inheritdoc/>
    public async Task<bool> LikeOrRepostAsync(RecordType recordType, string targetUri, string targetCid)
    {
        if ((recordType is not RecordType.Like && recordType is not RecordType.Repost) ||
            string.IsNullOrEmpty(targetUri) || 
            string.IsNullOrEmpty(targetCid))
        {
            return false;
        }

        var token = await _authenticationService.TryGetFreshTokenAsync();
        var handle = _userSettings.Get<string>(UserSettingsConstants.LastUsedUserHandleKey);

        if (token is null || handle is null)
        {
            return false;
        }

        SubmissionRecord newRecord = new()
        {
            CreatedAt = DateTime.Now,
            Subject = new RecordSubject
            {
                Uri = targetUri,
                Cid = targetCid
            }
        };

        CreateRecordResponse? result = await _blueskyApiClient.SubmitPostAsync(token, handle, newRecord, recordType);
        if (result is not null)
        {
            RecordCreated?.Invoke(this, (newRecord, result));
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


        SubmissionRecord newRecord = new()
        {
            CreatedAt = DateTime.Now,
            Text = text
        };

        CreateRecordResponse? result = await _blueskyApiClient.SubmitPostAsync(token, handle, newRecord, RecordType.NewPost);
        if (result is not null)
        {
            RecordCreated?.Invoke(this, (newRecord, result));
        }

        return result?.Uri;
    }
}
