namespace Bluesky.NET.Models;

public class ReplyRecord
{
    public FeedPost? Parent { get; init; }

    public FeedPost? Root { get; init; }
}
