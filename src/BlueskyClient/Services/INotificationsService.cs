using Bluesky.NET.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueskyClient.Services
{
    public interface INotificationsService
    {
        Task<IReadOnlyList<Notification>> GetNotificationsAsync();
    }
}