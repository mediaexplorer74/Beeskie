using Bluesky.NET.Models;
using System.Threading.Tasks;

namespace BlueskyClient.Services
{
    public interface IProfileService
    {
        Task<Author?> GetCurrentUserAsync();
    }
}