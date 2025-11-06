using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Order
{
    public class UpdateFirmaPorPedidoRequest
    {
        public int IdPedido { get; set; }
        public string FirmaBase64 { get; set; }
    }
 }