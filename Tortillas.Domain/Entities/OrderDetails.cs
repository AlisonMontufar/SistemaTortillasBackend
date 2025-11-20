using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tortillas.Domain.Entities
{
    public class DetallePedido
    {
        public int Id { get; set; }

        [Column("FkPedido")]           // Mapea correctamente la columna
        public int? FkPedido { get; set; }

        [ForeignKey("FkPedido")]        // Indica que Pedido es la relación de FkPedido
        public Pedido Pedido { get; set; }

        public string? ProductoNombre { get; set; }
        public decimal Cantidad { get; set; }
        
        public DateTime? FechaUltimaModificacion { get; set; }
        public DateTime? FechaHora { get; set; }
        public string? EstatusDetalle { get; set; }

        public string? Firma { get; set; }
    }
}
