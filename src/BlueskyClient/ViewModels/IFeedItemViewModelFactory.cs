using Bluesky.NET.Models;

namespace BlueskyClient.ViewModels;

public interface IFeedItemViewModelFactory
{
    FeedItemViewModel CreateViewModel(FeedItem feedItem);
}