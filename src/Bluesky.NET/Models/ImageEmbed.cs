namespace Bluesky.NET.Models;

public class ImageEmbed
{
    public required string Thumb { get; init; }

    public required string Fullsize { get; init; }

    public string Alt { get; init; } = string.Empty;

    public AspectRatio? AspectRatio { get; init; }
}
