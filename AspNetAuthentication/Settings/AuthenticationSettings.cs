namespace AspNetAuthentication.Settings
{
    public class AuthenticationSettings : IAuthenticationSettings
    {
        public string JwtKey { get; set; }

        public string JatIssuer { get; set; }

        public int JwtExpireDays { get; set; }
    }
}
