using System;

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

        // Llave foránea hacia Empresa
        public int FkEmpresa { get; set; }


    }
}
