using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Domain.Entities
{
    public class Empresa
    {
        public int Id { get; set; }

        public string? NombreEmpresa { get; set; }

        public string? Telefono { get; set; }

        public string? CorreoEmpresa { get; set; }

        public byte Estatus { get; set; } = 1;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public int? FkEmpresaPadre { get; set; }
    }
}
