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

    public ProfileService(
        ICache<Author> profileCache,
        IUserSettings userSettings)
    {
        _profileCache = profileCache;
        _userSettings = userSettings;
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
}
