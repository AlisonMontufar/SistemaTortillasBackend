using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Auth;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Domain.Interfaces.Services.Auth;

namespace Tortillas.Application.UseCases.Auth
{
    public class LoginUser
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginUser(IUserRepository userRepository, IAuthService authService, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResponse?> HandleAsync(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Identificador))
                return null;

            Usuario? user;

            if (request.Identificador.Contains("@"))
            {
                
                user = await _userRepository.GetByEmailAsync(request.Identificador);
            }
            else
            {
              
                user = await _userRepository.GetByUsernameAsync(request.Identificador);
            }

            if (user == null || !_authService.VerifyPassword(request.ContrasenaUsuario, user.ContrasenaUsuario))
                return null;

            var token = _jwtTokenService.GenerateToken(user.NombreUsuario, user.FkRol);

            return new LoginResponse
            {
                Username = user.NombreUsuario,
                RoleId = user.FkRol,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(2)
            };
        }
    }
}