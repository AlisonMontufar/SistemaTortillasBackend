using System;

namespace Tortillas.Application.Dtos.Auth
{
    public class RegisterRequest
    {
        public string NombreUsuario { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string ApellidoP { get; set; } = null!;
        public string ApellidoM { get; set; } = null!;
        public string CorreoUsuario { get; set; } = null!;
        public string ContrasenaUsuario { get; set; } = null!;
        public string TelefonoUsuario { get; set; } = null!;
        public string? PlacasVehiculo { get; set; } = null;
        public int? Empresa { get; set; } = null;
        public int? Rol { get; set; } = null ;
        public byte Estatus { get; set; } = 1;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
