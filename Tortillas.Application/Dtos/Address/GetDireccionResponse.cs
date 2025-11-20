using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Address
{
    public class GetDireccionResponse
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CP { get; set; }

        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public string?  Referencias { get; set; }

        public DateTime FechaUltimaModificacion { get; set; }
    }
}
