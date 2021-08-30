namespace LittleByte.Extensions.AspNet.Options
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public int TtlMinutes { get; set; } = 60;
    }
}