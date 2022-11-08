using DogHairSystem.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DogHairSystem.Core.Jwt
{
    public class TokenService : ITokenService
    {
        private const double DURATION_MINUTES = 30;
        public string BuildToken(string key, string issuer, User user)
        {
            var claims = new[] {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("UserId", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.NameIdentifier,
            Guid.NewGuid().ToString())
        };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.AddMinutes(DURATION_MINUTES), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public string GetClaimValue(string token, string claim)
        {
            string value = string.Empty;
            Claim foundClaim = null;
            var _token = new JwtSecurityToken(jwtEncodedString: token);
            if(_token != null)
            {
                foundClaim = _token.Claims.Where(c => c.Type == claim).FirstOrDefault();
                if(foundClaim != null)
                {
                    value = foundClaim.Value;
                }
            }
            return value;
        }

        public bool IsTokenValid(string key, string issuer, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public enum DHRoles
        {
            StandartUser = 1,
            Admin = 2
        }
    }
}
