using BlueskyClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BlueskyClient.ViewModels;

public partial class NotificationsPageViewModel : ObservableObject
{
    private readonly INotificationsService _notificationsService;
    private readonly INotificationViewModelFactory _notificationViewModelFactory;

    public NotificationsPageViewModel(
        INotificationsService notificationsService,
        INotificationViewModelFactory notificationViewModelFactory)
    {
        _notificationsService = notificationsService;
        _notificationViewModelFactory = notificationViewModelFactory;
    }

    public ObservableCollection<NotificationViewModel> Notifications { get; } = [];

    public async Task InitializeAsync()
    {
        var notifications = await _notificationsService.GetNotificationsAsync();
        foreach (var n in notifications)
        {
            var vm = _notificationViewModelFactory.CreateViewModel(n);
            Notifications.Add(vm);
        }
    }
}
