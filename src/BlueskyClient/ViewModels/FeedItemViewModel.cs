using Bluesky.NET.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueskyClient.ViewModels;

public partial class FeedItemViewModel : ObservableObject
{
    public FeedItemViewModel(FeedItem feedItem)
    {
        FeedItem = feedItem;
    }

    public FeedItem FeedItem { get; }

    public string AuthorHandle => $"@{FeedItem.Post.Author.Handle}";
}
