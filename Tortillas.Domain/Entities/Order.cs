using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tortillas.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }

        [Column("FkUsuario")]
        public int FkUsuario { get; set; }

        [ForeignKey("FkUsuario")]
        public Usuario Usuario { get; set; }  // Propiedad de navegación correctamente mapeada

        public int FkEmpresa { get; set; }

        [ForeignKey("FkEmpresa")]
        public Empresa Empresa { get; set; }

        public string EstatusGeneral { get; set; }

        public ICollection<DetallePedido> Detalles { get; set; } // EF Core sabe que es la relación inversa

    }
}
