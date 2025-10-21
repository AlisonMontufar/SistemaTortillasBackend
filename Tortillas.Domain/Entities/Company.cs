using System;
using System.Collections.Generic;

namespace Tortillas.Domain.Entities
{
    public class Empresa
    {
        public int Id { get; set; }
        public string NombreEmpresa { get; set; } = string.Empty;
        public string? Logo { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public byte Estatus { get; set; } = 1;

        // 🔹 Relación: una empresa tiene muchas sucursales
        public ICollection<Sucursal> Sucursales { get; set; } = new List<Sucursal>();
    }

}
