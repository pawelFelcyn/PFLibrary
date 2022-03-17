namespace AspNetAuthentication.Settings
{
    public class AuthenticationSettings : IAuthenticationSettings
    {
        public string JwtKey { get; set; }

        public string JwtIssuer { get; set; }

        public int JwtExpireDays { get; set; }
    }
}
