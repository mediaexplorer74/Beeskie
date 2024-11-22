using System.Collections.Generic;

namespace BlueskyClient.Constants;

public sealed class UserSettingsConstants
{
    /// <summary>
    /// Remembers the last user handle that signed in.
    /// </summary>
    public const string LastUsedUserHandleKey = "LastUsedUserHandle";

    /// <summary>
    /// Anonymous ID referencing the local user, for telemetry purposes.
    /// </summary>
    public const string LocalUserIdKey = "LocalUserId";

    /// <summary>
    ///  Settings defaults.
    /// </summary>
    public static IReadOnlyDictionary<string, object> Defaults { get; } = new Dictionary<string, object>()
    {
        { LastUsedUserHandleKey, string.Empty },
    };
}
