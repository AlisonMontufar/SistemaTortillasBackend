using System;
using System.Threading.Tasks;
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

        public RegisterUser(IUserRepository repo, IVehiculoRepository vehiculoRepo, IAuthService auth)
        {
            _repo = repo;
            _vehiculoRepo = vehiculoRepo;
            _auth = auth;
        }

        public async Task<Usuario?> HandleAsync(RegisterRequest request)
        {
            var existingUser = await _repo.GetByUsernameAsync(request.NombreUsuario);
            if (existingUser != null) return null;

            var existingEmail = await _repo.GetByEmailAsync(request.CorreoUsuario);
            if (existingEmail != null) return null;

            int? fkVehiculo = null;
            if (!string.IsNullOrWhiteSpace(request.PlacasVehiculo))
            {
                var vehiculo = await _vehiculoRepo.GetByPlacasAsync(request.PlacasVehiculo);
                if (vehiculo != null)
                {
                    fkVehiculo = vehiculo.Id;
                }
            }

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
