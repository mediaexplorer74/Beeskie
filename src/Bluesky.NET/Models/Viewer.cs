namespace Bluesky.NET.Models;

public class Viewer
{
    public string? Like { get; init; }

    public string? Repost { get; init; }

    public bool ThreadMuted { get; init; }

    public bool EmbeddingDisabled { get; init; }

    public bool Pinned { get; init; }

    public bool ReplyDisabled { get; init; }
}
