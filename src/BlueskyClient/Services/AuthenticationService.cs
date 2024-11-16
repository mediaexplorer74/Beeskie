using Bluesky.NET.ApiClients;
using Bluesky.NET.Models;
using BlueskyClient.Tools;
using System;
using System.Threading.Tasks;

namespace BlueskyClient.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    private const int TokenHoursToLive = 2; // based on decoded JWT, TTL is 2 hours.
    private readonly IBlueskyApiClient _apiClient;
    private readonly ISecureCredentialStorage _secureCredentialStorage;
    private string? _accesToken;
    private string? _refreshToken;
    private DateTime? _expirationTime;
    private string? _signedInUsername;

    public AuthenticationService(
        IBlueskyApiClient blueskyApiClient,
        ISecureCredentialStorage secureCredentialStorage)
    {
        _apiClient = blueskyApiClient;
        _secureCredentialStorage = secureCredentialStorage;
    }

    /// <inheritdoc/>
    public async Task<bool> TrySilentSignInAsync(string storedUserHandle)
    {
        var storedRefreshToken = _secureCredentialStorage.GetCredential(storedUserHandle);
        if (storedRefreshToken is not { Length: > 0 } oldRefreshToken)
        {
            return false;
        }

        var authResponse = await _apiClient.RefreshAsync(oldRefreshToken);
        if (authResponse?.Success is true)
        {
            _signedInUsername = storedUserHandle;
        }

        UpdateStoredToken(storedUserHandle, authResponse);

        return authResponse?.Success is true;
    }

    /// <inheritdoc/>
    public async Task<AuthResponse?> SignInAsync(string rawUserHandle, string rawPassword)
    {
        var handle = rawUserHandle.Trim();
        var password = rawPassword.Trim();

        if (string.IsNullOrEmpty(handle) || string.IsNullOrEmpty(password))
        {
            return null;
        }


        var result = await _apiClient.AuthenticateAsync(handle, password);
        if (result?.Success is true)
        {
            _signedInUsername = handle;
        }

        UpdateStoredToken(handle, result);

        return result;
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
            UpdateStoredToken(_signedInUsername, authResponse);
        }

        if (DateTime.Now < _expirationTime.Value && _accesToken is string token)
        {
            return token;
        }

        // Everything failed, so return null.
        return null;
    }

    private void UpdateStoredToken(string? userHandle, AuthResponse? response)
    {
        if (response is { Success: true, AccessJwt: string { Length: > 0 } accessToken, RefreshJwt: string { Length: > 0 } refreshToken })
        {
            _accesToken = accessToken;
            _refreshToken = refreshToken;
            _expirationTime = DateTime.Now.AddHours(TokenHoursToLive);

            if (userHandle is not null)
            {
                _secureCredentialStorage.SetCredential(userHandle, refreshToken);
            }
        }
    }
}
