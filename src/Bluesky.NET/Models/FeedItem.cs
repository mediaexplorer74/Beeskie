using System;
using System.Collections.Generic;
using System.Text;

namespace Bluesky.NET.Models;

// src: https://docs.bsky.app/docs/api/app-bsky-feed-get-timeline

public class FeedItem
{
    public required FeedPost Post { get; init; }
}
