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
    private readonly INavigator _rootNavigator;
    private readonly IProfileService _profileService;
    private readonly IDialogService _dialogService;
    private readonly IAuthenticationService _authenticationService;
    private MenuItem? _lastSelectedMenu;

    public ShellPageViewModel(
        INavigator contentNavigator,
        INavigator rootNavigator,
        IProfileService profileService,
        IDialogService dialogService,
        IAuthenticationService authenticationService)
    {
        _contentNavigator = contentNavigator;
        _rootNavigator = rootNavigator;
        _profileService = profileService;
        _dialogService = dialogService;
        _authenticationService = authenticationService;

        MenuItems.Add(new MenuItem(NavigateContentPageCommand, "Home", "\uEA8A", NavigationConstants.HomePage));
        MenuItems.Add(new MenuItem(NavigateContentPageCommand, "Notifications", "\uEA8F", NavigationConstants.NotificationsPage));
    }

    public ObservableCollection<MenuItem> MenuItems = [];

    [ObservableProperty]
    public Author? _currentUser;

    public async Task InitializeAsync(string? storedHandle = null)
    {
        if (storedHandle is not null)
        {
            var signInSuccessful = await _authenticationService.TrySilentSignInAsync(storedHandle);
            if (!signInSuccessful)
            {
                await _dialogService.OpenSignInRequiredAsync();

                // go back to sign in page
                _rootNavigator.NavigateTo(NavigationConstants.SignInPage);
                return;
            }
        }

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

    [RelayCommand]
    private async Task NewPostAsync()
    {
        await _dialogService.OpenPostDialogAsync();
    }
}
