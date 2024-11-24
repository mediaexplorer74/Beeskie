using BlueskyClient.Constants;
using BlueskyClient.Views;
using JeniusApps.Common.Settings;
using JeniusApps.Common.Telemetry;
using JeniusApps.Common.Tools;
using Microsoft.Extensions.DependencyInjection;
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

sealed partial class App : Application
{
    private static Frame? AppFrame;

    public static bool IsRightToLeftLanguage
    {
        get
        {
            string flowDirectionSetting 
                = ResourceContext.GetForCurrentView().QualifierValues["LayoutDirection"];
            return flowDirectionSetting == "RTL";
        }
    }

    public App()
    {
        this.InitializeComponent();
    }

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        try
        {
            await ActivateAsync(args);
        }
        catch { }
    }

    private Task ActivateAsync(LaunchActivatedEventArgs args)
    {
        _serviceProvider = default;

        try
        {
            _serviceProvider = ConfigureServices();
        }
        catch { }

        if (Window.Current.Content is not Frame rootFrame)
        {
            rootFrame = new();
            Window.Current.Content = rootFrame;
        }

        AppFrame = rootFrame;

        INavigator navigator = Services.GetRequiredKeyedService<INavigator>(
                NavigationConstants.RootNavigatorKey);
      
        navigator.SetFrame(rootFrame);

        if (args.PrelaunchActivated is false)
        {
            CoreApplication.EnablePrelaunch(true);

            if (rootFrame.Content is null)
            {
                string? storedHandle = default;

                try
                {
                    storedHandle = Services.GetRequiredService<IUserSettings>()
                        .Get<string>(UserSettingsConstants.LastUsedUserHandleKey);
                }
                catch { }

                if (string.IsNullOrEmpty(storedHandle) || storedHandle?.Contains("@") is true)
                {
                    rootFrame.Navigate(typeof(SignInPage));
                }
                else
                {
                    rootFrame.Navigate(typeof(ShellPage), storedHandle);
                }
            }

            Window.Current.Activate();

            try
            {
                ConfigureUI();
            }
            catch { }

            try
            {
                Services.GetRequiredService<ITelemetry>().TrackEvent(TelemetryConstants.Launched);
            }
            catch { }
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

            try
            {
                CustomizeTitleBar(darkTheme: rootFrame.ActualTheme is ElementTheme.Dark);
            }
            catch { }
        }
    }

    private void CustomizeTitleBar(bool darkTheme)
    {
        try
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar viewTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.ButtonBackgroundColor = Colors.Transparent;
            viewTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            viewTitleBar.ButtonForegroundColor = darkTheme ? Colors.LightGray : Colors.Black;
        }
        catch { }
    }
}
