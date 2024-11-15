using Bluesky.NET.ApiClients;
using Bluesky.NET.Constants;
using Bluesky.NET.Models;
using BlueskyClient.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueskyClient.Caches;

public class ProfileCache : ICache<Author>
{
    private readonly IBlueskyApiClient _apiClient;
    private readonly IAuthenticationService _authenticationService;
    private readonly ConcurrentDictionary<string, CachedItem<Author>> _cache = new();

    public ProfileCache(
        IBlueskyApiClient blueskyApiClient,
        IAuthenticationService authenticationService)
    {
        _apiClient = blueskyApiClient;
        _authenticationService = authenticationService;
    }

    public async Task<Author?> GetItemAsync(string handle)
    {
        if (_cache.TryGetValue(handle, out CachedItem<Author> cachedResult) &&
            DateTime.Now < cachedResult.ExpirationTime)
        {
            return cachedResult.Data;
        }

        // At this point, either the data doesn't exist or it's expired.
        // Regardless, get fresh data.

        var accessToken = await _authenticationService.TryGetFreshTokenAsync();
        if (accessToken is null)
        {
            return null;
        }

        Author? author = await _apiClient.GetAuthorAsync(accessToken, handle);
        if (author is null)
        {
            return null;
        }

        var newCachedItem = new CachedItem<Author>
        {
            Data = author,
            ExpirationTime = DateTime.Now.AddHours(UrlConstants.OnlineDataHoursToLive)
        };

        _cache.AddOrUpdate(handle, newCachedItem, (key, item) => newCachedItem);
        return author;
    }

    public Task<IReadOnlyDictionary<string, Author>> GetItemsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyDictionary<string, Author>> GetItemsAsync(IReadOnlyList<string> ids)
    {
        throw new NotImplementedException();
    }
}
