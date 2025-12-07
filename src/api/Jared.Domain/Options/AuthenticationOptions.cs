namespace Jared.Domain.Options;

public class AuthenticationOptions
{
    public static string Section { get; } = "Authentication";

    public string JwtKey { get; set; } = default!;
    public string JwtIssurer { get; set; } = default!;
    public int JwtExpireDays { get; set; }
}
