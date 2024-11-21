using Bluesky.NET.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueskyClient.Services;

public interface IProfileService
{
    Task<Author?> GetCurrentUserAsync();
    Task<IReadOnlyList<FeedItem>> GetProfileFeedAsync(string handle);
}