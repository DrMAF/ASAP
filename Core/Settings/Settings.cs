namespace Core.Settings
{
    public class AppSettings
    {
    }

    public class AuthSettings
    {
        public string TokenSecret { get; set; } = string.Empty;
        public string RefreshTokenSecret { get; set; } = string.Empty;
        public double TokenExpirationMunites { get; set; }
        public double RefreshTokenExpirationMunites { get; set; }
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
