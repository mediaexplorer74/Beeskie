using BlueskyClient.Views;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable enable

namespace BlueskyClient;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
sealed partial class App : Application
{
    private static Frame? AppFrame;

    public static bool IsRightToLeftLanguage
    {
        get
        {
            string flowDirectionSetting = ResourceContext.GetForCurrentView().QualifierValues["LayoutDirection"];
            return flowDirectionSetting == "RTL";
        }
    }

    public App()
    {
        this.InitializeComponent();
    }

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        await ActivateAsync(args);
    }

    private Task ActivateAsync(LaunchActivatedEventArgs args)
    {
        _serviceProvider = ConfigureServices();

        if (Window.Current.Content is not Frame rootFrame)
        {
            rootFrame = new();
            Window.Current.Content = rootFrame;
        }

        AppFrame = rootFrame;

        if (args.PrelaunchActivated is false)
        {
            CoreApplication.EnablePrelaunch(true);

            if (rootFrame.Content is null)
            {
                rootFrame.Navigate(typeof(SignInPage));
            }

            Window.Current.Activate();
            ConfigureUI();
        }

        return Task.CompletedTask;
    }

    private void ConfigureUI()
    {
        if (AppFrame is { } rootFrame)
        {
            if (IsRightToLeftLanguage)
            {
                rootFrame.FlowDirection = FlowDirection.RightToLeft;
            }

            CustomizeTitleBar(darkTheme: rootFrame.ActualTheme is ElementTheme.Dark);
        }
    }

    private void CustomizeTitleBar(bool darkTheme)
    {
        CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

        var viewTitleBar = ApplicationView.GetForCurrentView().TitleBar;
        viewTitleBar.ButtonBackgroundColor = Colors.Transparent;
        viewTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        viewTitleBar.ButtonForegroundColor = darkTheme ? Colors.LightGray : Colors.Black;
    }
}
