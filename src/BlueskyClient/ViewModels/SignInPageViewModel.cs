using BlueskyClient.Constants;
using BlueskyClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JeniusApps.Common.Settings;
using JeniusApps.Common.Telemetry;
using JeniusApps.Common.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueskyClient.ViewModels;

public partial class SignInPageViewModel : ObservableObject
{
    private readonly IAuthenticationService _authService;
    private readonly INavigator _navigator;
    private readonly IUserSettings _userSettings;
    private readonly ITelemetry _telemetry;

    public SignInPageViewModel(
        IAuthenticationService authenticationService,
        INavigator navigator,
        IUserSettings userSettings,
        ITelemetry telemetry)
    {
        _authService = authenticationService;
        _navigator = navigator;
        _userSettings = userSettings;
        _telemetry = telemetry;

        UserHandleInput = userSettings.Get<string>(UserSettingsConstants.LastUsedUserHandleKey) ?? string.Empty;
    }

    [ObservableProperty]
    private bool _signingIn;

    [ObservableProperty]
    private string _userHandleInput = string.Empty;

    [ObservableProperty]
    private string _appPasswordInput = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ErrorBannerVisible))]
    private string _signInErrorMessage = string.Empty;

    public bool ErrorBannerVisible => SignInErrorMessage.Length > 0;

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task SignInAsync()
    {
        SigningIn = true;

        var result = await _authService.SignInAsync(UserHandleInput, AppPasswordInput);

        SignInErrorMessage = result?.Success is true
            ? string.Empty
            : result?.ErrorMessage ?? "Null response";

        if (result is { Success: true, Handle: string { Length: > 0 } handle })
        {
            OnSuccessfulSignIn(handle);
        }

        SigningIn = false;

        _telemetry.TrackEvent(
            result?.Success is true ? TelemetryConstants.AuthSuccessFromSignInPage : TelemetryConstants.AuthFailFromSignInPage,
            new Dictionary<string, string>
            {
                { "userInputContainsAtSymbol", UserHandleInput.Contains("@").ToString() },
                { "handleContainsAtSymbol", result?.Handle?.Contains("@").ToString() ?? "NullHandle" },
            });
    }

    private void OnSuccessfulSignIn(string userHandle)
    {
        if (userHandle.Trim() is { Length: > 0 } handle)
        {
            _userSettings.Set(UserSettingsConstants.LastUsedUserHandleKey, handle);
        }

        _navigator.NavigateTo(NavigationConstants.ShellPage);
    }
}
