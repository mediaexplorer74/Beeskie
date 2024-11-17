using Bluesky.NET.Constants;
using Bluesky.NET.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace BlueskyClient.ViewModels;

public partial class NotificationViewModel : ObservableObject
{
    public NotificationViewModel(
        Notification notification)
    {
        Notification = notification;
    }

    public Notification Notification { get; }

    public bool AvatarValid => Uri.IsWellFormedUriString(Notification.Author.Avatar, UriKind.Absolute);

    public string SafeAvatarUrl => AvatarValid ? Notification.Author.Avatar : "http://local";

    public string Reason => Notification.Reason;

    public string CaptionString => Reason switch
    {
        ReasonConstants.Follow => $"{Notification.Author.DisplayName} followed you",
        ReasonConstants.Like => $"{Notification.Author.DisplayName} liked your post",
        ReasonConstants.Repost => $"{Notification.Author.DisplayName} reposted your post",
        ReasonConstants.Reply => "Posted a reply",
        _ => string.Empty
    };

    public bool IsLike => Reason is ReasonConstants.Like;

    public bool IsRepost => Reason is ReasonConstants.Repost;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SubjectText))]
    private FeedPost? _subjectPost;

    public string SubjectText => SubjectPost?.Record?.Text ?? string.Empty;
}
