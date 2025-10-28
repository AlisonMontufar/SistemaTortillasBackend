using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Interfaces.Services.Auth;


namespace Tortillas.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public bool VerifyPassword(string providedPassword, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(providedPassword) || string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }
}