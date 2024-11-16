using Bluesky.NET.Models;
using BlueskyClient.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueskyClient.Services
{
    public interface INotificationsService
    {
        Task<IReadOnlyList<Notification>> GetNotificationsAsync();
        Task HydrateAsync(NotificationViewModel notification);
    }
}