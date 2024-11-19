using System;
using System.Text.Json.Serialization;

namespace Bluesky.NET.Models;

public class FeedRecord
{
    [JsonPropertyName("$type")]
    public string Type { get; init; } = string.Empty;

    public DateTime CreatedAt { get; init; }

    public string Text { get; init; } = string.Empty;

    public ReplyRecord? Reply { get; init; }

    public PostEmbed[]? Embeds { get; init; }

    public Author? Author { get; init; }

    public FeedRecord? Value { get; init; }
}
