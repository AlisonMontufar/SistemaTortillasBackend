using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tortillas.Domain.Interfaces.Services.Auth;

namespace Tortillas.Application.Services
{
    public class RegistrationLinkService : IRegistrationLinkService
    {
  
        private const string SecretKey = "EsteEsUnSecretoMuySeguroDe32Caracteres";

        public string GenerateToken(string email, int roleId, int expirationMinutes = 30)
        {
            var claims = new[]
            {
                new Claim("email", email),
                new Claim("roleId", roleId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Tortillas",
                audience: "Tortillas",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string token, out string email, out int roleId)
        {
            email = null;
            roleId = 0;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "Tortillas",
                    ValidAudience = "Tortillas",
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero // sin margen extra
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                email = jwtToken.Claims.First(x => x.Type == "email").Value;
                roleId = int.Parse(jwtToken.Claims.First(x => x.Type == "roleId").Value);

                return true;
            }
            catch
            {
                return false; // token inválido o expirado
            }
        }
    }
}
