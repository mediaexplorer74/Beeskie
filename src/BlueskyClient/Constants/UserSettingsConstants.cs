using System.Collections.Generic;

namespace BlueskyClient.Constants;

public sealed class UserSettingsConstants
{
    /// <summary>
    /// Remembers the last user handle that signed in.
    /// </summary>
    public const string LastUsedUserHandleKey = "LastUsedUserHandle";

    /// <summary>
    ///  Settings defaults.
    /// </summary>
    public static IReadOnlyDictionary<string, object> Defaults { get; } = new Dictionary<string, object>()
    {
        { LastUsedUserHandleKey, string.Empty },
    };
}
