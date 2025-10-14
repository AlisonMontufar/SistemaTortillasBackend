using System;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Domain.Interfaces.Services.Auth;

namespace Tortillas.Application.UseCases.Auth
{
    public class PasswordRecovery
    {
        private readonly IUserRepository _userRepo;

        public PasswordRecovery(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<(bool success, string code)> GenerateRecoveryCodeAsync(string email)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user == null) return (false, null);

            var code = new Random().Next(100000, 999999).ToString();
            user.CodigoVerificacion = code;
            user.FechaExpiracionCodigoV = DateTime.UtcNow.AddMinutes(15);

            await _userRepo.UpdateAsync(user);
            return (true, code);
        }

        public async Task<bool> VerifyRecoveryCodeAsync(string email, string code)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user == null) return false;

            return user.CodigoVerificacion == code && user.FechaExpiracionCodigoV > DateTime.UtcNow;
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword, IAuthService authService)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user == null || user.FechaExpiracionCodigoV <= DateTime.UtcNow)
                return false;

            user.ContrasenaUsuario = authService.HashPassword(newPassword);
            user.CodigoVerificacion = null;
            user.FechaExpiracionCodigoV = null;

            await _userRepo.UpdateAsync(user);
            return true;
        }
    }
}