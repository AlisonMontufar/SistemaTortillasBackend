using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tortillas.Application.Dtos.Order
{
    public class UpdatePedidoRequest : IRequest<bool>
    {
        public int Id { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal Total { get; set; }

        public List<DetallePedidoDto> Detalles { get; set; } = new List<DetallePedidoDto>();
        public PagoDto Pago { get; set; }
    }
}
