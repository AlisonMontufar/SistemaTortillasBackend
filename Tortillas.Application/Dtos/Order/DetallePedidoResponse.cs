using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Order
{
    public class DetallePedidoResponse
    {
        public int Id { get; set; }
        public string ProductoNombre { get; set; }
        public decimal Cantidad { get; set; }
        public string EstatusNombre { get; set; }
        public List<int> SucursalesAsignadas { get; set; } = new();
        public string? EstatusDetalle { get; set; }

    }

}
