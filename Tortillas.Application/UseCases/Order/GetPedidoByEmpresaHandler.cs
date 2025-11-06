using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Order;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Order
{
    public class GetPedidosByEmpresaHandler
    {
        private readonly IPedidoRepository _repository;

        public GetPedidosByEmpresaHandler(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DetallePedidoEmpresaDto>> Handle(GetPedidosByEmpresaRequest request, CancellationToken cancellationToken)
        {
            var raw = await _repository.GetPedidosByEmpresaAsync(request.EmpresaId);

            return raw.Select(x => new DetallePedidoEmpresaDto
            {
                IdPedido = x.IdPedido,
                Empresa = x.Empresa,
                NombreEncargado = x.NombreEncargado,
                Sucursal = x.Sucursal,
                EstatusGeneral = x.EstatusGeneral,
                EstatusDetalle = x.EstatusDetalle,
                FechaHora = x.FechaHora,
                Cantidad = x.Cantidad,
                Total = x.Total,
                Producto = x.Producto,

                Calle = x.Calle,
                Numero = x.Numero,
                Colonia = x.Colonia,
                CodigoPostal = x.CodigoPostal,
                Ciudad = x.Ciudad,
                Estado = x.Estado,
              
            });
        }
    }
}