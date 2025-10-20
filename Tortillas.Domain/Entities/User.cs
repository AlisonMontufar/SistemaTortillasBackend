using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tortillas.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string CorreoUsuario { get; set; } = null!;
        public string ContrasenaUsuario { get; set; } = null!;
        public string TelefonoUsuario { get; set; } = null!;
        public int? FkEmpresa { get; set; } = null;
        public int FkRol { get; set; }
        public byte Estatus { get; set; } = 1;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public string Nombre { get; set; } = null!;
        public string ApellidoP { get; set; } = null!;
        public string ApellidoM { get; set; } = null!;
        public int? FkVehiculo { get; set; } = null;

        public string? CodigoVerificacion { get; set; }
        public DateTime? FechaExpiracionCodigoV { get; set; }
    }
}