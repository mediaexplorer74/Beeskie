namespace Bluesky.NET.Models;

// https://docs.bsky.app/docs/api/app-bsky-notification-list-notifications

public class Notification
{
    public required string Cid { get; init; }

    public required Author Author { get; init; }

    public required string Reason { get; init; }

    public required bool IsRead { get; init; }
}
