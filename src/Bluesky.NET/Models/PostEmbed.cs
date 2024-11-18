using System.Text.Json.Serialization;

namespace Bluesky.NET.Models;

public class PostEmbed
{
    [JsonPropertyName("$type")]
    public required string Type { get; init; }

    public ImageEmbed[]? Images { get; init; }

    public ExternalEmbed? External { get; init; }
}
