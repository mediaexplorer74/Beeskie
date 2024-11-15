using System.Text.Json.Serialization;

namespace Bluesky.NET.Models;

public class Author
{
    public required string Did { get; init; }

    public required string Handle { get; init; }

    public string DisplayName { get; init; } = string.Empty;

    public string Avatar { get; init; } = string.Empty;

    public string? Description { get; init; }

    public int? FollowersCount { get; init; }

    public int? FollowsCount { get; init; }

    public int? PostsCount { get; init; }

    [JsonIgnore]
    public string AtHandle => $"@{Handle}";
}
