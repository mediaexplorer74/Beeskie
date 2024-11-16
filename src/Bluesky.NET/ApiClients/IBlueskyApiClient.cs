using Bluesky.NET.Constants;
using Bluesky.NET.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bluesky.NET.ApiClients;

public interface IBlueskyApiClient
{
    /// <summary>
    /// Retrieves authenticated tokens that can be used
    /// for other API calls that require auth.
    /// </summary>
    /// <param name="userHandle">The user's handle or email address.</param>
    /// <param name="appPassword">An app password provided by the user.</param>
    /// <returns>An <see cref="AuthResponse"/>.</returns>
    Task<AuthResponse?> AuthenticateAsync(string userHandle, string appPassword);
    Task<IReadOnlyList<FeedItem>> GetTimelineAsync(string accesstoken);
    Task<AuthResponse?> RefreshAsync(string refreshToken);

    Task<Author?> GetAuthorAsync(string accessToken, string handle);

    Task<IReadOnlyList<Notification>> GetNotificationsAsync(string accessToken);
    Task<IReadOnlyList<FeedPost>> GetPostsAsync(string accessToken, IReadOnlyList<string> atUriList);
    Task<CreateRecordResponse?> SubmitPostAsync(string accessToken, string handle, SubmissionRecord record, RecordType recordType);
}