using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tortillas.Application.Dtos.Order
{
    public class CreatePedidoRequest : IRequest<int>
    {
        public int FkSucursal { get; set; }
        public int FkUsuario { get; set; }
        public int FkDireccion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal Total { get; set; }

        public List<DetallePedidoDto> Detalles { get; set; } = new List<DetallePedidoDto>();
        public PagoDto Pago { get; set; }
    }
}
