namespace LittleByte.AspNet;

public record JwtOptions(string Issuer, string Audience, string Secret, int TtlMinutes = 60);