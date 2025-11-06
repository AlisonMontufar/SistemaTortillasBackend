using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Order
{
    public class UpdatePedidoRequest
    {
        public int Id { get; set; }
        public string EstatusGeneral { get; set; }
        public decimal Total { get; set; }
    }
}
