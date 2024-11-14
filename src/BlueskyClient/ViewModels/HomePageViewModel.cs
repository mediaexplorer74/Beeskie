using BlueskyClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace BlueskyClient.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    private readonly ITimelineService _timelineService;
    private readonly IFeedItemViewModelFactory _feedItemViewModelFactory;

    public HomePageViewModel(
        ITimelineService timelineService,
        IFeedItemViewModelFactory feedItemViewModelFactory)
    {
        _timelineService = timelineService;
        _feedItemViewModelFactory = feedItemViewModelFactory;
    }

    public ObservableCollection<FeedItemViewModel> FeedItems { get; } = [];

    public async Task InitializeAsync()
    {
        var items = await _timelineService.GetTimelineAsync();
        foreach (var item in items)
        {
            var vm = _feedItemViewModelFactory.CreateViewModel(item);
            FeedItems.Add(vm);
        }
    }
}
