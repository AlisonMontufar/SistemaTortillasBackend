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
        public string NombreEmpresa { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public int Estatus { get; set; } = 1;

    }
}
