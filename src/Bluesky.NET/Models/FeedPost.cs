using System;

namespace Bluesky.NET.Models;

// src: https://docs.bsky.app/docs/api/app-bsky-feed-get-timeline

public class FeedPost
{
    public required string Uri { get; init; }

    public string Cid { get; init; } = string.Empty;

    public DateTime IndexedAt { get; init; }

    public int ReplyCount { get; init; }

    public int RepostCount { get; init; }

    public int LikeCount { get; init; }

    public int QuoteCount { get; init; }

    public FeedRecord? Record { get; init; }

    public PostEmbed? Embed { get; init; }

    public Author? Author { get; init; }

    public Viewer? Viewer { get; init; }
}
