﻿using BlueskyClient.Constants;
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
        var navigator = Services.GetRequiredKeyedService<INavigator>(NavigationConstants.RootNavigatorKey);
        navigator.SetFrame(rootFrame);

        if (args.PrelaunchActivated is false)
        {
            CoreApplication.EnablePrelaunch(true);

            if (rootFrame.Content is null)
            {
                var storedHandle = Services.GetRequiredService<IUserSettings>().Get<string>(UserSettingsConstants.LastUsedUserHandleKey);

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
            ConfigureUI();
            Services.GetRequiredService<ITelemetry>().TrackEvent(TelemetryConstants.Launched);
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
