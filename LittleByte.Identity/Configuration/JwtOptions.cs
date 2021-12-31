namespace LittleByte.Identity.Configuration;

public record JwtOptions(string Issuer, string Audience, string Secret, int TtlMinutes = 60);