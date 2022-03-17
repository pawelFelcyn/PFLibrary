namespace AspNetAuthentication.Settings
{
    public interface IAuthenticationSettings
    {
        public string JwtKey { get; }
        public string JwtIssuer { get; }
        public int JwtExpireDays { get; }
    }
}
