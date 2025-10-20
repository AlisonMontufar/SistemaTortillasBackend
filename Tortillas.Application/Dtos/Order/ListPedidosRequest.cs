using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tortillas.Application.Dtos.Order
{
    public class ListPedidosRequest : IRequest<List<PedidoDto>>
    {
        // Opcional: filtros como UsuarioId o EmpresaId
        public int? FkUsuario { get; set; }
        public int? FkEmpresa { get; set; }
    }
}
