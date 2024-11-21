using Bluesky.NET.Models;
using BlueskyClient.Extensions;
using BlueskyClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlueskyClient.ViewModels;

public partial class ProfileControlViewModel : ObservableObject
{
    private readonly IProfileService _profileService;
    private readonly IFeedItemViewModelFactory _feedItemViewModelFactory;

    public ProfileControlViewModel(
        IProfileService profileService,
        IFeedItemViewModelFactory feedItemViewModelFactory)
    {
        _profileService = profileService;
        _feedItemViewModelFactory = feedItemViewModelFactory;
    }

    public ObservableCollection<FeedItemViewModel> FeedItems { get; } = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SafeAvatarUrl))]
    [NotifyPropertyChangedFor(nameof(IsBannerVisible))]
    private Author? _author;

    public string SafeAvatarUrl => Author.SafeAvatarUrl();

    public bool IsBannerVisible => Author?.Banner is not null;

    public async Task InitializeCurrentUserProfileAsync(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        Author = await _profileService.GetCurrentUserAsync();
        if (Author is null)
        {
            return;
        }
        ct.ThrowIfCancellationRequested();

        var feedItems = await _profileService.GetProfileFeedAsync(Author.Handle);
        foreach (var f in feedItems)
        {
            ct.ThrowIfCancellationRequested();
            var vm = _feedItemViewModelFactory.CreateViewModel(f);
            FeedItems.Add(vm);
        }
    }
}
