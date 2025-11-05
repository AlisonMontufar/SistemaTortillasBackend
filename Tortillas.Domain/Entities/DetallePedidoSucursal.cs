using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tortillas.Domain.Entities
{
    public class DetallePedidoSucursal
    {
        public int Id { get; set; }

        public int FkDetallePedido { get; set; }
        public int FkSucursal { get; set; }

        public DateTime? FechaAsignacion { get; set; }

        // Relaciones
        [ForeignKey("FkDetallePedido")]
        public DetallePedido? DetallePedido { get; set; }

        [ForeignKey("FkSucursal")]
        public Sucursal? Sucursal { get; set; }
    }
}
