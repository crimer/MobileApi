using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentResults;
using Microsoft.IdentityModel.Tokens;

namespace MobileApi.Core.Services.Auth
{
    public class AuthService
    {
        public Result<string> GetToken(string jwtSecretKey, string userName, string userPassword)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));    
            var credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("password", userPassword),
                new("name", userName)
            };

            var token = new JwtSecurityToken(null , null, permClaims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);
            
            return Result.Ok(jwtTokenHandler.WriteToken(token));
        }
    }
}