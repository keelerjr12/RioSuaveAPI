using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace RioSuaveLib.JWT
{
    public class JwtService
    {
        public JwtService(string issuer, string audience, SymmetricSecurityKey signingKey)
        {
            _issuer = issuer;
            _audience = audience;
            _signingKey = signingKey;
        }

        public JwtDTO Generate()
        {
            var header = new JwtHeader(new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256));
            var payload = new JwtPayload(_issuer, _audience, null, DateTime.UtcNow, DateTime.Now.AddHours(6));

            var jwt = new JwtSecurityToken(header, payload);
            var encryptedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDTO { Token = encryptedJwt };
        }

        private readonly string _issuer;
        private readonly string _audience;
        private readonly SymmetricSecurityKey _signingKey;
    }
}
