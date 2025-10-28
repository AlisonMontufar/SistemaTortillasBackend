using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Domain.Entities
{
    public class DetallePedido
    {
        public int Id { get; set; }
        public int FkPedido { get; set; }
        public int? FkDireccion { get; set; }
        public string? ProductoNombre { get; set; }
        public decimal Cantidad { get; set; }
        public string? EstatusNombre { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public DateTime FechaHora { get; set; }
        public string? NombreSucursal { get; set; }
    }
}
