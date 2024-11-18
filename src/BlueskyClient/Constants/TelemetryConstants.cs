namespace BlueskyClient.Constants;

public sealed class TelemetryConstants
{
    private const string Error = "error:";
    public const string ApiError = Error + "apiError";

    private const string App = "app:";
    public const string Launched = App + "launched";
}
