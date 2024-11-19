using Bluesky.NET.Constants;
using Bluesky.NET.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BlueskyClient.ViewModels;

public partial class NotificationViewModel : ObservableObject
{
    public NotificationViewModel(
        Notification notification)
    {
        Notification = notification;
    }

    public Notification Notification { get; }

    public bool AvatarValid => IsAvatarValid(Notification.Author);

    public string SafeAvatarUrl => IsAvatarValid(Notification.Author) ? Notification.Author.Avatar : "http://local";

    public string Reason => Notification.Reason;

    public string CaptionString
    {
        get
        {
            if (IsAvatarValid(Notification.Author))
            {
                return Reason switch
                {
                    ReasonConstants.Follow => $"{Notification.Author.DisplayName} followed you",
                    ReasonConstants.Like => $"{Notification.Author.DisplayName} liked your post",
                    ReasonConstants.Repost => $"{Notification.Author.DisplayName} reposted your post",
                    ReasonConstants.Reply => "Posted a reply",
                    _ => string.Empty
                };
            }

            return string.Empty;
        }
    }

    public bool IsLike => Reason is ReasonConstants.Like;

    public bool IsRepost => Reason is ReasonConstants.Repost;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SubjectText))]
    private FeedPost? _subjectPost;

    public string SubjectText => SubjectPost?.Record?.Text ?? string.Empty;

    private bool IsAvatarValid([NotNullWhen(true)] Author? author) =>
        author?.Avatar is string avatarUrl &&
        Uri.IsWellFormedUriString(avatarUrl, UriKind.Absolute);
}
