using System;
using System.Collections.Generic;
using System.Text;

namespace Bluesky.NET.Models;

public class FeedResponse
{
    public FeedItem[]? Feed { get; init; }
    public Notification[]? Notifications { get; init; }

    public FeedPost[]? Posts { get; init; }
}
