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
    public class DeletePedidoHandler : IRequestHandler<DeletePedidoRequest, bool>
    {
        private readonly IPedidoRepository _pedidoRepo;
        private readonly IDetallePedidoRepository _detalleRepo;

        public DeletePedidoHandler(IPedidoRepository pedidoRepo, IDetallePedidoRepository detalleRepo)
        {
            _pedidoRepo = pedidoRepo;
            _detalleRepo = detalleRepo;
        }

        public async Task<bool> Handle(DeletePedidoRequest request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepo.GetPedidoByIdAsync(request.PedidoId);
            if (pedido == null) return false;

            // Eliminar detalles
            var detalles = await _detalleRepo.GetAllDetallesAsync(request.PedidoId);
            foreach (var d in detalles)
            {
                await _detalleRepo.DeleteDetalleAsync(d.Id);
            }

            // Eliminar pedido
            await _pedidoRepo.DeletePedidoAsync(request.PedidoId);
            return true;
        }
    }
}
