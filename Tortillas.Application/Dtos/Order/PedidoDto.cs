using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.OrderDetails;
using Tortillas.Application.Dtos.Pay;

namespace Tortillas.Application.Dtos.Order
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public int FkSucursal { get; set; }
        public int FkUsuario { get; set; }
        public int FkDireccion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }

        public List<DetallePedidoDto> Detalles { get; set; } = new();
        public PagoDto? Pago { get; set; }
    }
}
