using System.Text.Json.Serialization;

namespace Bluesky.NET.Models;

public class AuthRequestBody
{
    public required string Identifier { get; init; }

    public required string Password { get; init; }
}
