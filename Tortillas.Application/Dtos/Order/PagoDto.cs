using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Order
{
    public class PagoDto
    {
        public int Id { get; set; }
        public int FkPedido { get; set; }
        public string? NombreTitular { get; set; }
        public string MetodoPago { get; set; } = "Tarjeta";
        public string? NumeroEnmascarado { get; set; }
        public string? MarcaTarjeta { get; set; }
        public byte? ExpMes { get; set; }
        public short? ExpAnio { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

}
