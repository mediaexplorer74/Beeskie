using Bluesky.NET.Models;
using BlueskyClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace BlueskyClient.ViewModels;

public partial class FeedItemViewModel : ObservableObject
{
    private readonly IPostSubmissionService _postSubmissionService;

    public FeedItemViewModel(
        FeedItem feedItem,
        IPostSubmissionService postSubmissionService)
    {
        FeedItem = feedItem;
        _postSubmissionService = postSubmissionService;
    }

    public FeedItem FeedItem { get; }

    [ObservableProperty]
    private bool _isLiked;

    [RelayCommand]
    private async Task LikeAsync()
    {
        if (IsLiked)
        {
            return;
        }

        var result = await _postSubmissionService.LikeAsync(
            FeedItem.Post.Uri,
            FeedItem.Post.Cid);

        IsLiked = result;
    }
}
