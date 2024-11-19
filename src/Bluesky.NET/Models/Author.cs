using System;
using System.Text.Json.Serialization;

namespace Bluesky.NET.Models;

public class Author
{
    public string Did { get; init; } = string.Empty;

    public string Handle { get; init; } = string.Empty;

    public string DisplayName { get; init; } = string.Empty;

    public string Avatar { get; init; } = string.Empty;

    public string? Description { get; init; }

    public int? FollowersCount { get; init; }

    public int? FollowsCount { get; init; }

    public int? PostsCount { get; init; }

    [JsonIgnore]
    public string AtHandle => $"@{Handle}";
}
