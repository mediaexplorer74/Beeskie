using Bluesky.NET.Constants;
using Bluesky.NET.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace BlueskyClient.ViewModels;

public partial class NotificationViewModel : ObservableObject
{
    public NotificationViewModel(Notification notification)
    {
        Notification = notification;
    }

    public Notification Notification { get; }

    public bool AvatarValid => Uri.IsWellFormedUriString(Notification.Author.Avatar, UriKind.Absolute);

    public string SafeAvatarUrl => AvatarValid ? Notification.Author.Avatar : "http://local";

    public string Reason => Notification.Reason;

    public string FollowString => Reason is ReasonConstants.Follow
        ? $"{Notification.Author.DisplayName} followed you"
        : string.Empty;
}
