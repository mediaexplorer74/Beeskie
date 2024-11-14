using BlueskyClient.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

#nullable enable

namespace BlueskyClient.Views;

public sealed partial class SignInPage : Page
{
    public SignInPage()
    {
        this.InitializeComponent();
        ViewModel = App.Services.GetRequiredService<SignInPageViewModel>();
    }

    public SignInPageViewModel ViewModel { get; }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        await ViewModel.InitializeAsync();
    }
}
