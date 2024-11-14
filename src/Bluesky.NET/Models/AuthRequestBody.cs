using System.Text.Json.Serialization;

namespace Bluesky.NET.Models;

public class AuthRequestBody
{
    [JsonPropertyName("identifier")]
    public required string Identifier { get; init; }

    [JsonPropertyName("password")]
    public required string Password { get; init; }
}
