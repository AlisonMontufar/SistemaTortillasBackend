using System;

namespace Tortillas.Application.Dtos.Order
{
    public class DetallePedidoEmpresaDto
    {
        public int Id { get; set; }
        public int? IdPedido { get; set; }
        public string Empresa { get; set; }
        public string NombreEncargado { get; set; }
        public string Sucursal { get; set; }
        public string EstatusGeneral { get; set; }
        public string EstatusDetalle { get; set; }
        public DateTime? FechaHora { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Total { get; set; }
        public string Producto { get; set; }

        // Dirección
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
   
    }
}