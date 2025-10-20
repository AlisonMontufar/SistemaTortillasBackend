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
    public class UpdatePedidoHandler : IRequestHandler<UpdatePedidoRequest, bool>
    {
        private readonly IPedidoRepository _pedidoRepo;
        private readonly IDetallePedidoRepository _detalleRepo;

        public UpdatePedidoHandler(IPedidoRepository pedidoRepo, IDetallePedidoRepository detalleRepo)
        {
            _pedidoRepo = pedidoRepo;
            _detalleRepo = detalleRepo;
        }

        public async Task<bool> Handle(UpdatePedidoRequest request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepo.GetPedidoByIdAsync(request.Id);
            if (pedido == null) return false;

            pedido.FechaEntrega = request.FechaEntrega;
            pedido.Total = request.Total;
            pedido.FechaUltimaModificacion = DateTime.UtcNow;

            await _pedidoRepo.UpdatePedidoAsync(pedido);

            // Actualizar detalles
            foreach (var detalle in request.Detalles)
            {
                var existing = await _detalleRepo.GetDetalleByIdAsync(detalle.Id);
                if (existing != null)
                {
                    existing.ProductoNombre = detalle.ProductoNombre;
                    existing.Cantidad = detalle.Cantidad;
                    existing.NombreSucursal = detalle.NombreSucursal;
                    existing.FechaHora = detalle.FechaHora;
                    existing.EstatusNombre = detalle.EstatusNombre;
                    existing.FechaUltimaModificacion = DateTime.UtcNow;

                    await _detalleRepo.UpdateDetalleAsync(existing);
                }
            }

            return true;
        }
    }
}
