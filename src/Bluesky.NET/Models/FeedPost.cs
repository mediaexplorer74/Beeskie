using System;

namespace Bluesky.NET.Models;

// src: https://docs.bsky.app/docs/api/app-bsky-feed-get-timeline

public class FeedPost
{
    public required string Uri { get; init; }

    public required string Cid { get; init; }

    public required DateTime IndexedAt { get; init; }

    public int ReplyCount { get; init; }

    public int RepostCount { get; init; }

    public int LikeCount { get; init; }

    public int QuoteCount { get; init; }

    public required FeedRecord Record { get; init; }

    public required Author Author { get; init; }

    public Viewer? Viewer { get; init; }
}
