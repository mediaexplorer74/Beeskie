using Bluesky.NET.Models;

namespace BlueskyClient.Extensions;

public static class PostExtensions
{
    public static string GetReplyCount(this FeedPost post) => GetPostButtonIconString(post.ReplyCount);

    public static string GetRepostCount(this FeedPost post) => GetPostButtonIconString(post.RepostCount);

    public static string GetLikeCount(this FeedPost post) => GetPostButtonIconString(post.LikeCount);

    private static string GetPostButtonIconString(int count)
    {
        return count <= 0
            ? string.Empty
            : count.ToString();
    }
}
