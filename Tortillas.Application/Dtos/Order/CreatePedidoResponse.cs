using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Order
{
    public class CreatePedidoResponse
    {
        public int PedidoId { get; set; }
        public string Mensaje { get; set; } = "Pedido creado correctamente";
    }
}
