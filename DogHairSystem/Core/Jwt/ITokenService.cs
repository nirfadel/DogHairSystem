using DogHairSystem.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHairSystem.Core.Jwt
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, User user);
        bool IsTokenValid(string key, string issuer, string token);
        string GetClaimValue(string token, string claim);
    }
}
