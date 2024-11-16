namespace Bluesky.NET.Constants;

public static class RecordTypes
{
    public const string NewPost = "app.bsky.feed.post";
    public const string Like = "app.bsky.feed.like";

    public static string ToStringType(this RecordType recordType) => recordType switch
    {
        RecordType.NewPost => NewPost,
        RecordType.Like => Like,
        _ => string.Empty,
    };
}

public enum RecordType
{
    NewPost,

    Like
}
