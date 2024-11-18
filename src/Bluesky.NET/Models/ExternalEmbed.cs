using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Bluesky.NET.Models;

public class ExternalEmbed
{
    public required string Uri { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string Thumb { get; init; } = string.Empty;

    [JsonIgnore]
    public string RootUri => System.Uri.TryCreate(Uri, UriKind.Absolute, out var uri)
        ? $"{uri.Scheme}://{uri.Host}"
        : string.Empty;
}
