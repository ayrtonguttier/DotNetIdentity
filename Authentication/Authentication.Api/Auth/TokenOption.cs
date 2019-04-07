using Microsoft.IdentityModel.Tokens;
using System;


namespace Authentication.Api.Auth
{
    public static class TokenOption
    {
        public static string Issuer { get { return "EUMESMO"; } }
        public static string Audience { get { return "TODOS"; } }
        public static SecurityKey Key
        {
            get
            {
                return new RsaSecurityKey( RSAKeyUtils.GenerateIfNotExists());
            }
        }
        public static SigningCredentials SigningCredentials
        {
            get
            {
                return new SigningCredentials(Key, SecurityAlgorithms.RsaSha256);
            }
        }
        public static TimeSpan ExpiresSpan { get { return TimeSpan.FromMinutes(15); } }
    }
}
