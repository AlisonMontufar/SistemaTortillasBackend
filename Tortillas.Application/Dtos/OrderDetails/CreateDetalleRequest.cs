using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.OrderDetails
{
    public class CreateDetalleRequest
    {
        public int FkDireccion { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public string? EstatusNombre { get; set; }
        public DateTime FechaHora { get; set; }
        public string? NombreSucursal { get; set; }
    }

}
