namespace Bluesky.NET.Constants;

public static class RecordTypes
{
    public const string NewPost = "app.bsky.feed.post";
    public const string Like = "app.bsky.feed.like";
    public const string Repost = "app.bsky.feed.repost";

    public static string ToStringType(this RecordType recordType) => recordType switch
    {
        RecordType.Reply => NewPost,
        RecordType.NewPost => NewPost,
        RecordType.Like => Like,
        RecordType.Repost => Repost,
        _ => string.Empty,
    };
}

public enum RecordType
{
    NewPost,
    Like,
    Repost,
    Reply,
}
