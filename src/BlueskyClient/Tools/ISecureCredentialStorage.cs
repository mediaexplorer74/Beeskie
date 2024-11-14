using System;
using System.Collections.Generic;
using System.Text;

namespace BlueskyClient.Tools;

public interface ISecureCredentialStorage
{
    public bool SetCredential(string key, string credential);

    public string? GetCredential(string key);
}
