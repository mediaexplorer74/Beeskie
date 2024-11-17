using BlueskyClient.Constants;
using BlueskyClient.ViewModels;
using JeniusApps.Common.Tools;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

#nullable enable

namespace BlueskyClient.Views;

public sealed partial class ShellPage : Page
{
    public ShellPage()
    {
        this.InitializeComponent();
        ViewModel = App.Services.GetRequiredService<ShellPageViewModel>();
    }

    public ShellPageViewModel ViewModel { get; }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        App.Services.GetRequiredKeyedService<INavigator>(NavigationConstants.ContentNavigatorKey).SetFrame(ContentFrame);
        _ = ViewModel.InitializeAsync(e.Parameter as string).ConfigureAwait(false);
    }
}
