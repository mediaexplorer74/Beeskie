using BlueskyClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueskyClient.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    private readonly ITimelineService _timelineService;

    public HomePageViewModel(ITimelineService timelineService)
    {
        _timelineService = timelineService;
    }

    public async Task InitializeAsync()
    {
        var items = await _timelineService.GetTimelineAsync();
    }
}
