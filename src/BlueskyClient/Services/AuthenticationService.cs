using Bluesky.NET.ApiClients;
using Bluesky.NET.Models;
using System;
using System.Threading.Tasks;

namespace BlueskyClient.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    private const int TokenHoursToLive = 2; // based on decoded JWT, TTL is 2 hours.
    private readonly IBlueskyApiClient _apiClient;
    private string? _accesToken;
    private string? _refreshToken;
    private DateTime? _expirationTime;

    public AuthenticationService(IBlueskyApiClient blueskyApiClient)
    {
        _apiClient = blueskyApiClient;
    }

    public async Task<AuthResponse?> SignInAsync(string rawUserHandle, string rawPassword)
    {
        var handle = rawUserHandle.Trim();
        var password = rawPassword.Trim();

        if (string.IsNullOrEmpty(handle) || string.IsNullOrEmpty(password))
        {
            return null;
        }


        var result = await _apiClient.AuthenticateAsync(handle, password);
        UpdateStoredToken(result);

        return result;
    }

    private void UpdateStoredToken(AuthResponse? response)
    {
        if (response is { Success: true, AccessJwt: string { Length: > 0 } accessToken, RefreshJwt: string { Length: > 0 } refreshToken })
        {
            _accesToken = accessToken;
            _refreshToken = refreshToken;
            _expirationTime = DateTime.Now.AddHours(TokenHoursToLive);
        }
        else
        {
            _accesToken = null;
            _refreshToken = null;
            _expirationTime = null;
        }
    }

    public async Task<string?> TryGetFreshTokenAsync()
    {
        if (_accesToken is null || _refreshToken is null || _expirationTime is null)
        {
            // Not initialized at all. Need to perform sign in.
            return null;
        }

        if (DateTime.Now >= _expirationTime.Value && _refreshToken is string refreshToken)
        {
            var authResponse = await _apiClient.RefreshAsync(refreshToken);
            UpdateStoredToken(authResponse);
        }

        if (DateTime.Now < _expirationTime.Value && _accesToken is string token)
        {
            return token;
        }

        // Everything failed, so return null.
        return null;
    }
}
