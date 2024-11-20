using Bluesky.NET.Models;
using BlueskyClient.Constants;
using BlueskyClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JeniusApps.Common.Models;
using JeniusApps.Common.Tools;
using System.Collections.Generic;
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
    private readonly IImageViewerService _imageViewerService;
    private MenuItem? _lastSelectedMenu;

    public ShellPageViewModel(
        INavigator contentNavigator,
        INavigator rootNavigator,
        IProfileService profileService,
        IDialogService dialogService,
        IAuthenticationService authenticationService,
        IImageViewerService imageViewerService)
    {
        _contentNavigator = contentNavigator;
        _rootNavigator = rootNavigator;
        _profileService = profileService;
        _dialogService = dialogService;
        _authenticationService = authenticationService;
        _imageViewerService = imageViewerService;

        MenuItems.Add(new MenuItem(NavigateContentPageCommand, "Home", "\uEA8A", NavigationConstants.HomePage));
        MenuItems.Add(new MenuItem(NavigateContentPageCommand, "Notifications", "\uEA8F", NavigationConstants.NotificationsPage));
    }

    public ObservableCollection<MenuItem> MenuItems = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsImageViewerVisible))]
    private IReadOnlyList<ImageEmbed>? _images;

    public bool IsImageViewerVisible => Images is { Count: > 0 };

    [ObservableProperty]
    private int _imageViewerIndex;

    [ObservableProperty]
    private Author? _currentUser;

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

        _imageViewerService.ImageViewerRequested += OnImageViewerRequested;

        Task<Author?> profileTask = _profileService.GetCurrentUserAsync();
        NavigateContentPage(MenuItems[0]);
        CurrentUser = await profileTask;
    }

    public void Unitialize()
    {
        _imageViewerService.ImageViewerRequested -= OnImageViewerRequested;
    }

    private void OnImageViewerRequested(object sender, ImageViewerArgs args)
    {
        if (args.Images.Count == 0)
        {
            return;
        }

        ImageViewerIndex = args.LaunchIndex < args.Images.Count ? args.LaunchIndex : 0;
        Images = args.Images;
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

    [RelayCommand]
    private void CloseImageViewer()
    {
        Images = null;
        ImageViewerIndex = 0;
    }
}
