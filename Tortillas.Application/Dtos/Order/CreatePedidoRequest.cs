using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.OrderDetails;
using Tortillas.Application.Dtos.Pay;


namespace Tortillas.Application.Dtos.Order
{
    public class CreatePedidoRequest
    {
<<<<<<< Updated upstream
        public int FkSucursal { get; set; }
=======
        public int FkSucursal { get; set; }       
>>>>>>> Stashed changes
        public int FkUsuario { get; set; }
        public int FkDireccion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal Total { get; set; }

        public List<CreateDetalleRequest> Detalles { get; set; } = new();
        public CreatePagoRequest? Pago { get; set; }
    }
}
