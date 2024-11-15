using Bluesky.NET.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlueskyClient.ViewModels;

public partial class NotificationViewModel : ObservableObject
{
    public NotificationViewModel(Notification notification)
    {
        Notification = notification;
    }

    public Notification Notification { get; }
}
