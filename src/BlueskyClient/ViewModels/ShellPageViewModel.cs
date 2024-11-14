using BlueskyClient.Constants;
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
    private MenuItem? _lastSelectedMenu;

    public ShellPageViewModel(INavigator contentNavigator)
    {
        _contentNavigator = contentNavigator;

        MenuItems.Add(new MenuItem(NavigateContentPageCommand, "Home", "\uEA8A", NavigationConstants.HomePage));
        MenuItems.Add(new MenuItem(NavigateContentPageCommand, "Notifications", "\uEA8F", NavigationConstants.NotificationsPage));
    }

    public ObservableCollection<MenuItem> MenuItems = [];

    public async Task InitializeAsync()
    {
        await Task.Delay(1);
        NavigateContentPage(MenuItems[0]);
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
