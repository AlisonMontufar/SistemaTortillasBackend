using System;
using System.Collections.Generic;

namespace Tortillas.Application.Dtos.Order
{
    public class CreatePedidoResponse
    {
        public int Id { get; set; }
        public string EstatusGeneral { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public int FkUsuario { get; set; }
        public int FkEmpresa { get; set; }

        public List<DetallePedidoDto> Detalles { get; set; } = new List<DetallePedidoDto>();
        public PagoDto Pago { get; set; }
    }

    public class DetallePedidoDto
    {
        public int Id { get; set; }
        public string ProductoNombre { get; set; }
        public decimal Cantidad { get; set; }
        public string EstatusNombre { get; set; }
        public string EstatusDetalle { get; set; }
        public DateTime? FechaUltimaModificacion { get; set; }
        public DateTime? FechaHora { get; set; }
    }

    public class PagoDto
    {
        public int Id { get; set; }
        public string MetodoPago { get; set; }
        public string NumeroEnmascarado { get; set; }
        public string MarcaTarjeta { get; set; }
        public byte ExpMes { get; set; }
        public short ExpAnio { get; set; }
        public string NombreTitular { get; set; }
        public string TokenPago { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
