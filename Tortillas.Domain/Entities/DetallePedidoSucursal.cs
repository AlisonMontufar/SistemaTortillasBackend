using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Domain.Entities
{
    public class DetallePedidoSucursal
    {
        public int Id { get; set; }
        public int FkDetallePedido { get; set; }
        public int FkSucursal { get; set; }
        public DateTime? FechaAsignacion { get; set; }
    }
}
