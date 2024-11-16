using System;

namespace Bluesky.NET.Models;

public class FeedRecord
{
    public DateTime CreatedAt { get; init; }

    public string Text { get; init; } = string.Empty;
}
