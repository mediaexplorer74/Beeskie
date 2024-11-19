namespace Bluesky.NET.Models;

// https://docs.bsky.app/docs/api/app-bsky-notification-list-notifications

public class Notification
{
    public string Cid { get; init; } = string.Empty;

    public Author? Author { get; init; }

    public string Reason { get; init; } = string.Empty;

    public bool IsRead { get; init; }

    public string? ReasonSubject { get; init; }

    public FeedRecord? Record { get; init; }
}
