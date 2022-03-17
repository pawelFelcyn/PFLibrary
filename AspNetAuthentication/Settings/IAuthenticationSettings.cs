namespace AspNetAuthentication.Settings
{
    public interface IAuthenticationSettings
    {
        public string JwtKey { get; }
        public string JatIssuer { get; }
        public int JwtExpireDays { get; }
    }
}
