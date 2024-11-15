using Bluesky.NET.Models;
using BlueskyClient.Constants;
using BlueskyClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JeniusApps.Common.Models;
using JeniusApps.Common.Tools;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BlueskyClient.ViewModels;

public partial class ShellPageViewModel : ObservableObject
{
    private readonly INavigator _contentNavigator;
    private readonly IProfileService _profileService;
    private MenuItem? _lastSelectedMenu;

    public ShellPageViewModel(
        INavigator contentNavigator,
        IProfileService profileService)
    {
        _contentNavigator = contentNavigator;
        _profileService = profileService;

        MenuItems.Add(new MenuItem(NavigateContentPageCommand, "Home", "\uEA8A", NavigationConstants.HomePage));
        MenuItems.Add(new MenuItem(NavigateContentPageCommand, "Notifications", "\uEA8F", NavigationConstants.NotificationsPage));
    }

    public ObservableCollection<MenuItem> MenuItems = [];

    [ObservableProperty]
    public Author? _currentUser;

    public async Task InitializeAsync()
    {
        Task<Author?> profileTask = _profileService.GetCurrentUserAsync();
        NavigateContentPage(MenuItems[0]);
        CurrentUser = await profileTask;
    }

    [RelayCommand]
    private void NavigateContentPage(MenuItem? item)
    {
        if (item?.Tag is not string { Length: > 0 } key)
        {
            return;
        }

        if (_lastSelectedMenu is { } lastMenu)
        {
            lastMenu.IsSelected = false;
        }

        item.IsSelected = true;
        _lastSelectedMenu = item;
        _contentNavigator.NavigateTo(key);
    }
}
