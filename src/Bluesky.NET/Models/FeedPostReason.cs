using System.Text.Json.Serialization;

namespace Bluesky.NET.Models;

public class FeedPostReason
{
    [JsonPropertyName("$type")]
    public required string Type { get; init; }

    public Author? By { get; init; }
}