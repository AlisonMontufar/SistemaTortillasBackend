using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Order
{
    public class PagoRequest
    {
        public string MetodoPago { get; set; }
        public string NumeroEnmascarado { get; set; }
        public string MarcaTarjeta { get; set; }
        public byte ExpMes { get; set; }
        public short ExpAnio { get; set; }
        public string NombreTitular { get; set; }
        public string TokenPago { get; set; }
    }
}
