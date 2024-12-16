namespace BlueskyClient.Services
{
    public interface ISecureCredentialStorage
    {
        string? GetCredential(string storedDid);
        void SetCredential(string storedDid, string v);
    }
}