namespace Bluesky.NET.Models;

public class AuthResponse
{
    public bool Success { get; set; }
    public string? Handle { get; set; }
    public string? Email { get; set; }
    public bool? EmailConfirmed { get; set; }
    public bool? EmailAuthFactor { get; set; }
    public string? AccessJwt { get; set; }
    public string? RefreshJwt { get; set; }
    public bool? Active { get; set; }
    public string? ErrorMessage { get; set; }
}
