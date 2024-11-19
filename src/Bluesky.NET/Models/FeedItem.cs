namespace Bluesky.NET.Models;

// src: https://docs.bsky.app/docs/api/app-bsky-feed-get-timeline

public class FeedItem
{
    public FeedPost Post { get; init; } = new();

    public FeedPostReason? Reason { get; init; }

    public ReplyRecord? Reply { get; init; }
}
