using Bluesky.NET.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlueskyClient.ViewModels;

public partial class FeedItemViewModel : ObservableObject
{
    public FeedItemViewModel(FeedItem feedItem)
    {
        FeedItem = feedItem;
    }

    public FeedItem FeedItem { get; }
}
