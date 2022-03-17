using AspNetAuthentication.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspNetAuthentication.TokenGeneration
{
    public abstract class JwtTokenGeterator<TUser> : ITokenGenerator<TUser>
    {
        private readonly IAuthenticationSettings _authenticationSettings;
        private readonly Dictionary<string, Func<TUser, string>> _claimsInfo;

        public JwtTokenGeterator(IAuthenticationSettings authenticationSettings)
        {
            _authenticationSettings = authenticationSettings;
            _claimsInfo = new();
        }

        public string GetTokenString(TUser user)
        {
            var claims = GetClaims(user);
            var token = GenerateToken(claims);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        private IEnumerable<Claim> GetClaims(TUser user)
        {
            foreach (var info in _claimsInfo)
            {
                yield return new Claim(info.Key, info.Value(user));
            }
        }

        protected virtual JwtSecurityToken GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(_authenticationSettings.JwtExpireDays);

            return new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: credentials);
        }

        protected void IncludeInClaims(string name, Func<TUser, string> value)
        {
            _claimsInfo.Add(name, value);
        }

        protected bool TryIncludeInClaims(string name, Func<TUser, string> value)
        {
            return _claimsInfo.TryAdd(name, value);
        }
    }
}
