using System;
using System.Collections.Generic;
using System.Text;

namespace Bluesky.NET.Models;

public class FeedResponse
{
    public required FeedItem[] Feed { get; init; }
}
