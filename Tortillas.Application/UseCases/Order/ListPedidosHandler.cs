using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tortillas.Application.Dtos.Order;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Order
{
    public class ListPedidosHandler : IRequestHandler<ListPedidosRequest, List<PedidoDto>>
    {
        private readonly IPedidoRepository _pedidoRepo;
        private readonly IDetallePedidoRepository _detalleRepo;

        public ListPedidosHandler(IPedidoRepository pedidoRepo, IDetallePedidoRepository detalleRepo)
        {
            _pedidoRepo = pedidoRepo;
            _detalleRepo = detalleRepo;
        }

        public async Task<List<PedidoDto>> Handle(ListPedidosRequest request, CancellationToken cancellationToken)
        {
            var pedidos = await _pedidoRepo.GetAllPedidosAsync();
            var result = new List<PedidoDto>();

            foreach (var pedido in pedidos)
            {
                var detalles = await _detalleRepo.GetAllDetallesAsync(pedido.Id);

                result.Add(new PedidoDto
                {
                    Id = pedido.Id,
                    FkSucursal = pedido.FkSucursal,
                    FkUsuario = pedido.FkUsuario,
                    FkDireccion = pedido.FkDireccion,
                    FechaEntrega = pedido.FechaEntrega,
                    Total = pedido.Total,
                    FechaUltimaModificacion = pedido.FechaUltimaModificacion,
                    Detalles = detalles.Select(d => new DetallePedidoDto
                    {
                        Id = d.Id,
                        ProductoNombre = d.ProductoNombre,
                        Cantidad = d.Cantidad,
                        NombreSucursal = d.NombreSucursal,
                        FechaHora = d.FechaHora,
                        EstatusNombre = d.EstatusNombre
                    }).ToList()
                });
            }

            return result;
        }
    }
}
