using Bluesky.NET.Models;

namespace BlueskyClient.ViewModels
{
    public interface INotificationViewModelFactory
    {
        NotificationViewModel CreateViewModel(Notification notification);
    }
}