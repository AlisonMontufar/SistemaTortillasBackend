using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Order
{
    public class GetPedidoResponse
    {
        public int PedidoId { get; set; }
        public int FkEmpresa { get; set; }
        public int FkUsuario { get; set; }
        public int FkDireccion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public List<DetallePedidoDto> Detalles { get; set; } = new();
        public PagoViewDto? Pago { get; set; }
    }
}
