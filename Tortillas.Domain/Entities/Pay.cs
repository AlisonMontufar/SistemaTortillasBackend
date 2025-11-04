using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tortillas.Domain.Entities
{
    public class Pago
    {
        public int Id { get; set; }

        [Column("FkPedido")]
        public int FkPedido { get; set; }

        [ForeignKey("FkPedido")] // Esto le dice a EF Core que Pedido se enlaza a FkPedido
        public Pedido? Pedido { get; set; }

        public string MetodoPago { get; set; } = string.Empty;
        public string NumeroEnmascarado { get; set; }
        public string MarcaTarjeta { get; set; }
        public byte ExpMes { get; set; }
        public short ExpAnio { get; set; }
        public string NombreTitular { get; set; }
        public string TokenPago { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
