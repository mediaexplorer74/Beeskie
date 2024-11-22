﻿using Bluesky.NET.ApiClients;
using Bluesky.NET.Models;
using BlueskyClient.Caches;
using BlueskyClient.Constants;
using JeniusApps.Common.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueskyClient.Services;

public class ProfileService : IProfileService
{
    private readonly ICache<Author> _profileCache;
    private readonly IUserSettings _userSettings;
    private readonly IBlueskyApiClient _apiClient;
    private readonly IAuthenticationService _authenticationService;

    public ProfileService(
        ICache<Author> profileCache,
        IUserSettings userSettings,
        IBlueskyApiClient blueskyApiClient,
        IAuthenticationService authenticationService)
    {
        _profileCache = profileCache;
        _userSettings = userSettings;
        _apiClient = blueskyApiClient;
        _authenticationService = authenticationService;
    }

    public async Task<Author?> GetCurrentUserAsync()
    {
        var handle = _userSettings.Get<string>(UserSettingsConstants.LastUsedUserHandleKey);
        if (handle is null)
        {
            return null;
        }

        return await _profileCache.GetItemAsync(handle);
    }

    public async Task<IReadOnlyList<FeedItem>> GetProfileFeedAsync(string handle)
    {
        if (handle is not { Length: > 0 })
        {
            return [];
        }

        var token = await _authenticationService.TryGetFreshTokenAsync();
        if (token is not { Length: > 0 })
        {
            return [];
        }

        return await _apiClient.GetAuthorFeedAsync(token, handle);
    }
}
