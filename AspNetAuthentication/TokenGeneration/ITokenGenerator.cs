namespace AspNetAuthentication.TokenGeneration
{
    public interface ITokenGenerator<TUser>
    {
        string GetTokenString(TUser user);
    }
}
