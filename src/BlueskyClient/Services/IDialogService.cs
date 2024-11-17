using Bluesky.NET.Models;
using System.Threading.Tasks;

namespace BlueskyClient.Services;

public interface IDialogService
{
    Task OpenPostDialogAsync();

    Task OpenReplyDialogAsync(FeedPost target);

    Task OpenSignInRequiredAsync();
}
