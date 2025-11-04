using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Order
{
    public class PagoResponse
    {
        public int Id { get; set; }
        public string MetodoPago { get; set; }
        public string NumeroEnmascarado { get; set; }
        public string MarcaTarjeta { get; set; }
        public string NombreTitular { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
