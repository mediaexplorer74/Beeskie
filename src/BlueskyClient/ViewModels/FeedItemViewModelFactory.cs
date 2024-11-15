using Bluesky.NET.Models;
using System;

namespace BlueskyClient.ViewModels;

public class FeedItemViewModelFactory : IFeedItemViewModelFactory
{
    private readonly IServiceProvider _serviceProvider;

    public FeedItemViewModelFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public FeedItemViewModel CreateViewModel(FeedItem feedItem)
    {
        return new FeedItemViewModel(
            feedItem);
    }
}
