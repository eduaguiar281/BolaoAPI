namespace Bolao.Security.Settings
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int DurationMinutes { get; set; }
        public string Key { get; set; }
        public int FinalExpiration { get; set; }
        public int RefreshTokenValidityInDays { get; set; }
    }
}
