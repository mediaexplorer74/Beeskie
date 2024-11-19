using Bluesky.NET.Models;
using System;
using System.Diagnostics.CodeAnalysis;

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

    public static string SafeAvatarUrl([NotNullWhen(true)] this Author? author) => author?.Avatar is { Length: > 0 } avatar && Uri.IsWellFormedUriString(avatar, UriKind.Absolute)
        ? avatar
        : "http://localhost";

    public static string SafeAvatarUrl([NotNullWhen(true)] this FeedPost? post) => SafeAvatarUrl(post?.Author);

    public static string SafeAvatarUrl([NotNullWhen(true)] this FeedRecord? record) => SafeAvatarUrl(record?.Author);
}
