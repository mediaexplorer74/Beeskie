namespace Bluesky.NET.Models;

public class ImageEmbed
{
    public string Thumb { get; init; } = string.Empty;

    public string Fullsize { get; init; } = string.Empty;

    public string Alt { get; init; } = string.Empty;

    public AspectRatio? AspectRatio { get; init; }
}
