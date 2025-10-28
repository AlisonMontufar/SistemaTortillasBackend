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
                IdEmpresa = x.IdEmpresa,
                NombreEmpresa = x.NombreEmpresa,
                IdSucursal = x.IdSucursal,
                NombreSucursal = x.NombreSucursal,
                Calle = x.Calle,
                Numero = x.Numero,
                Colonia = x.Colonia,
                Ciudad = x.Ciudad,
                Estado = x.Estado,
                CP = x.CP,
                Referencias = x.Referencias,
                IdPedido = x.IdPedido,
                Total = x.Total,
                FechaEntrega = x.FechaEntrega,
                IdDetalle = x.IdDetalle,
                ProductoNombre = x.ProductoNombre,
                Cantidad = (int)x.Cantidad, // conversión explícita si es decimal
                EstatusNombre = x.EstatusNombre
            });
        }
    }
}