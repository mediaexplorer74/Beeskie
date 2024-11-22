using System;
using System.Diagnostics;
using Windows.Security.Credentials;

#nullable enable

namespace BlueskyClient.Tools.Uwp;

public sealed class SecureCredentialStorage : ISecureCredentialStorage
{
    private const string ResourceName = "blueskyClientCredentials";

    public bool SetCredential(string key, string credential)
    {
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(credential))
        {
            return false;
        }

        PasswordVault vault = new();
        vault.Add(new PasswordCredential(ResourceName, key, credential));
        return true;
    }

    public string? GetCredential(string key)
    {
        try
        {
            PasswordVault vault = new();
            PasswordCredential credential = vault.Retrieve(ResourceName, key);
            credential.RetrievePassword();
            return credential.Password;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null;
        }
    }
}
