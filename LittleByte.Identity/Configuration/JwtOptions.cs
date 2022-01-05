namespace LittleByte.Identity.Configuration;

public class JwtOptions
{
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string Secret { get; init; } = null!;
    public int TtlMinutes { get; set; } = 60;
}