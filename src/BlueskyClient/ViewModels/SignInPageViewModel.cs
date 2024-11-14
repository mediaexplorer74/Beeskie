using Bluesky.NET.ApiClients;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueskyClient.ViewModels;

public partial class SignInPageViewModel : ObservableObject
{
    private readonly IBlueskyApiClient _blueskyApiClient;

    public SignInPageViewModel(IBlueskyApiClient blueskyApiClient)
    {
        _blueskyApiClient = blueskyApiClient;
    }

    [ObservableProperty]
    private string _userHandleInput = string.Empty;

    [ObservableProperty]
    private string _appPasswordInput = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ErrorBannerVisible))]
    private string _signInErrorMessage = string.Empty;

    public bool ErrorBannerVisible => SignInErrorMessage.Length > 0;

    [RelayCommand]
    private async Task SignInAsync()
    {
        var handle = UserHandleInput.Trim();
        var password = AppPasswordInput.Trim();
        
        if (string.IsNullOrEmpty(handle) || string.IsNullOrEmpty(password))
        {
            return;
        }

        var result = await _blueskyApiClient.AuthenticateAsync(handle, password);

        SignInErrorMessage = result?.Success is true
            ? string.Empty
            : result?.ErrorMessage ?? "Null response";
    }
}
