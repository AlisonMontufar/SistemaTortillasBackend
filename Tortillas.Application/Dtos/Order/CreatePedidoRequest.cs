using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Order
{
        public class CreatePedidoRequest
        {
            
            public int FkEmpresa { get; set; }
            public int FkUsuario { get; set; }
            public decimal Total { get; set; }
            public string EstatusGeneral { get; set; } = "Pendiente";
            public List<DetallePedidoRequest> Detalles { get; set; } = new();
            public PagoRequest Pago { get; set; }
        }
    }

