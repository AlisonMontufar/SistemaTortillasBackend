using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }


        public int FkSucursal { get; set; }
        public int FkUsuario { get; set; }

        public int FkDireccion { get; set; }

        public DateTime FechaEntrega { get; set; }

        public decimal Total { get; set; }

        public DateTime FechaUltimaModificacion { get; set; }

        public List<DetallePedido> Detalles { get; set; } = new();
        public int? PagoId { get; set; }
        public Pago? Pago { get; set; }

      
    }
}
