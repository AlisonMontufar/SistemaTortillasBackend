using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Order
{
    public class PedidoResponse
    {
        public int Id { get; set; }
        public string EstatusGeneral { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public int FkUsuario { get; set; }
        public int FkEmpresa { get; set; }
        public List<DetallePedidoResponse> Detalles { get; set; } = new();
        public PagoResponse? Pago { get; set; }
    }
}
