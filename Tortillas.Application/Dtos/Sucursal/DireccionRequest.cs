using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Sucursal
{
    public class DireccionRequest
    {
        public string Calle { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Colonia { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string CP { get; set; } = string.Empty;
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public string? Referencias { get; set; }
    }
}
