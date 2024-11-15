using Bluesky.NET.Models;
using System;

namespace BlueskyClient.ViewModels;

public class NotificationViewModelFactory : INotificationViewModelFactory
{
    private readonly IServiceProvider _serviceProvider;

    public NotificationViewModelFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public NotificationViewModel CreateViewModel(Notification notification)
    {
        return new NotificationViewModel(
            notification);
    }
}
