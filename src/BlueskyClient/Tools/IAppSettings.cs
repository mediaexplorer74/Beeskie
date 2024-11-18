namespace BlueskyClient.Tools;

public interface IAppSettings
{
    /// <summary>
    /// API key for telemetry.
    /// </summary>
    string TelemetryApiKey { get; }
}
