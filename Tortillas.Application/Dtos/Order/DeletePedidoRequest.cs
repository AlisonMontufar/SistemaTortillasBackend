using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tortillas.Application.Dtos.Order
{
    public class DeletePedidoRequest : IRequest<bool>
    {
        public int PedidoId { get; set; }
    }
}
