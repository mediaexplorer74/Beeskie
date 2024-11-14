using BlueskyClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace BlueskyClient.ViewModels;

public partial class SignInPageViewModel : ObservableObject
{
    private readonly IAuthenticationService _blueskyApiClient;

    public SignInPageViewModel(IAuthenticationService blueskyApiClient)
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
        var result = await _blueskyApiClient.SignInAsync(UserHandleInput, AppPasswordInput);

        SignInErrorMessage = result?.Success is true
            ? string.Empty
            : result?.ErrorMessage ?? "Null response";

        if (result?.Success is true)
        {
            // navigate to shell
        }
    }
}
