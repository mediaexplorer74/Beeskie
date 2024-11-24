#nullable enable

using Windows.ApplicationModel.Resources;

namespace BlueskyClient.Tools.Uwp;

public class AppSettings : IAppSettings
{
    public AppSettings()
    {
        var resourceLoader = ResourceLoader.GetForCurrentView("appsettings");
        TelemetryApiKey = resourceLoader.GetString(nameof(TelemetryApiKey));
    }

    /// <inheritdoc/>
    public string TelemetryApiKey { get; }
}
