using Business.DTOs.Token;
using Business.Services.Abstracts;
using Entities.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public TokenDto CreateAccessToken(AppUser user)
        {
            TokenDto token = new();

            // Symmetric Security Key
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));

            // SigninCredentials
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            // Options
            token.Expiration = DateTime.UtcNow.AddSeconds(300);
            JwtSecurityToken jwtSecurityToken = new(
                issuer: "www.asd.com",
                audience: "www.api.com",
                notBefore: DateTime.UtcNow,
                expires: token.Expiration,
                signingCredentials: signingCredentials,
                claims: new List<Claim> { }
                );
            //token creater
            JwtSecurityTokenHandler tokenHandler = new();

            token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create(); // IDisposible
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
