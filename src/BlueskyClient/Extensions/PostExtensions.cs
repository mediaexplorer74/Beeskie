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

    public static string SafeAvatarUrl(this Author? author) => SafeUrl(author?.Avatar);

    public static string SafeAvatarUrl(this FeedPost? post) => SafeUrl(post?.Author?.Avatar);

    public static string SafeAvatarUrl(this FeedRecord? record) => SafeUrl(record?.Author?.Avatar);

    public static string FollowersCount(this Author? author) => (author?.FollowersCount ?? 0).ToString();

    public static string FollowsCount(this Author? author) => (author?.FollowsCount ?? 0).ToString();

    public static string PostsCount(this Author? author) => (author?.PostsCount ?? 0).ToString();

    public static string SafeBannerUrl(this Author? author) => SafeUrl(author?.Banner);

    public static Uri SafeBannerUri(this Author? author) => Uri.TryCreate(SafeBannerUrl(author), UriKind.Absolute, out var result)
        ? result
        : new Uri("http://localhost");

    private static string SafeUrl(this string? url) => 
        url is { Length: > 0 } safeUrl && Uri.IsWellFormedUriString(safeUrl, UriKind.Absolute)
        ? safeUrl
        : "http://localhost";
}
