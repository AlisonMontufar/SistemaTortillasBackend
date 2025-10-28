using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Pay
{
    public class PagoDto
    {
        public int Id { get; set; }
        public int FkPedido { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string? NumeroEnmascarado { get; set; }
        public string? MarcaTarjeta { get; set; }
        public byte? ExpMes { get; set; }
        public short? ExpAnio { get; set; }
        public string? NombreTitular { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
