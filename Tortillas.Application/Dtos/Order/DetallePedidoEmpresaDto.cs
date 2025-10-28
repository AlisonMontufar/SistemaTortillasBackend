using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Order
{
    public class DetallePedidoEmpresaDto
    {
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CP { get; set; }
        public string Referencias { get; set; }
        public int IdPedido { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int IdDetalle { get; set; }
        public string ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public string EstatusNombre { get; set; }
    }
}
