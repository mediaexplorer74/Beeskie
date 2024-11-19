using System.Text.Json.Serialization;

namespace Bluesky.NET.Models;

public class FeedPostReason
{
    [JsonPropertyName("$type")]
    public string Type { get; init; } = string.Empty;

    public Author? By { get; init; }
}