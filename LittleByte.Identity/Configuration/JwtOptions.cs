namespace LittleByte.Asp.Identity
{
    public record JwtOptions(string Issuer, string Audience, string Secret, int TtlMinutes = 60);
}