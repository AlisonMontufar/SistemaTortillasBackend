using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Interfaces.Services.Auth;

namespace Tortillas.Application.Services
{
    public class AuthApplicationService : IAuthService
    {
        private readonly IAuthService _inner;
        public AuthApplicationService(IAuthService inner) => _inner = inner;

        // Simple pass-through, te sirve si quieres añadir lógica extra más adelante.
        public string HashPassword(string password) => _inner.HashPassword(password);
        public bool VerifyPassword(string hashedPassword, string providedPassword) => _inner.VerifyPassword(hashedPassword, providedPassword);
    }
}
