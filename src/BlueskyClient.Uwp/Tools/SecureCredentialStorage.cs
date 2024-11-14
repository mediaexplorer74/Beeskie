using Windows.Security.Credentials;

#nullable enable

namespace BlueskyClient.Tools.Uwp;

public sealed class SecureCredentialStorage : ISecureCredentialStorage
{
    private const string ResourceName = "blueskyClientCredentials";
    private readonly PasswordVault _vault = new();

    public bool SetCredential(string key, string credential)
    {
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(credential))
        {
            return false;
        }

        _vault.Add(new PasswordCredential(ResourceName, key, credential));
        return true;
    }

    public string? GetCredential(string key)
    {
        PasswordCredential credential = _vault.Retrieve(ResourceName, key);
        credential.RetrievePassword();
        return credential.Password;
    }
}
