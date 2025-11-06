using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tortillas.Domain.Entities
{
    public class Sucursal
    {
        public int Id { get; set; }

        public string NombreSucursal { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public string? CorreoElectronico { get; set; }

        public string? NombreEncargado { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public byte Estatus { get; set; } = 1;

        // Claves foráneas
        public int FkDireccion { get; set; }
        public int FkEmpresa { get; set; }

        // Relaciones
        [ForeignKey("FkDireccion")]
        public Direccion? Direccion { get; set; }

        [ForeignKey("FkEmpresa")]
        public Empresa? Empresa { get; set; }
    }
}
