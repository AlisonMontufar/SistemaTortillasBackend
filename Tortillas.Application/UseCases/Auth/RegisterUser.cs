using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Tortillas.Application.Dtos.Auth;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Domain.Interfaces.Services.Auth;

namespace Tortillas.Application.UseCases.Auth
{
    public class RegisterUser
    {
        private readonly IUserRepository _repo;
        private readonly IVehiculoRepository _vehiculoRepo;
        private readonly IAuthService _auth;
        private readonly IRegistrationLinkService _linkService;

        public RegisterUser(
            IUserRepository repo,
            IVehiculoRepository vehiculoRepo,
            IAuthService auth,
            IRegistrationLinkService linkService)
        {
            _repo = repo;
            _vehiculoRepo = vehiculoRepo;
            _auth = auth;
            _linkService = linkService;
        }

        public async Task<Usuario?> HandleAsync(RegisterRequest request)
        {
            // 🔹 Si es admin o encargado, validar token
            if (request.Rol == 1 || request.Rol == 2)
            {
                if (string.IsNullOrEmpty(request.Token))
                    throw new Exception("El registro de administradores o encargados requiere un enlace válido.");

                if (!_linkService.ValidateToken(request.Token, out var email, out var roleId))
                    throw new Exception("Token inválido o expirado.");

                if (email != request.CorreoUsuario || roleId != request.Rol)
                    throw new Exception("El token no coincide con el correo o rol.");
            }

            // 🔹 Validar duplicados
            if (await _repo.GetByUsernameAsync(request.NombreUsuario) != null)
                throw new Exception("El nombre de usuario ya existe.");

            if (await _repo.GetByEmailAsync(request.CorreoUsuario) != null)
                throw new Exception("El correo ya está registrado.");

            // 🔹 Verificar si existe vehículo
            int? fkVehiculo = null;
            if (!string.IsNullOrWhiteSpace(request.PlacasVehiculo))
            {
                var vehiculo = await _vehiculoRepo.GetByPlacasAsync(request.PlacasVehiculo);
                if (vehiculo != null)
                    fkVehiculo = vehiculo.Id;
            }

            // 🔹 Crear usuario
            var usuario = new Usuario
            {
                NombreUsuario = request.NombreUsuario,
                Nombre = request.Nombre,
                ApellidoP = request.ApellidoP,
                ApellidoM = request.ApellidoM,
                ContrasenaUsuario = _auth.HashPassword(request.ContrasenaUsuario),
                CorreoUsuario = request.CorreoUsuario,
                TelefonoUsuario = request.TelefonoUsuario,
                FkEmpresa = request.Empresa,
                FkRol = request.Rol,
                FkVehiculo = fkVehiculo,
                Estatus = request.Estatus,
                FechaRegistro = request.FechaRegistro
            };

            return await _repo.AddAsync(usuario);
        }
    }
}