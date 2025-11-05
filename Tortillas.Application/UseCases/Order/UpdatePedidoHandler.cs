using System;
using System.Threading.Tasks;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Application.Dtos.Order;

namespace Tortillas.Application.UseCases.Order
{
    public class UpdatePedidoHandler
    {
        private readonly IPedidoRepository _pedidoRepository;

        public UpdatePedidoHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PedidoResponse?> Handle(UpdatePedidoRequest request)
        {
            // Buscar pedido
            var pedido = await _pedidoRepository.GetByIdAsync(request.Id);
            if (pedido == null)
                return null;

            // Actualizar valores
            pedido.EstatusGeneral = request.EstatusGeneral;
            pedido.Total = request.Total;
            pedido.FechaUltimaModificacion = DateTime.Now;

            // Guardar cambios
            await _pedidoRepository.UpdateAsync(pedido);

            // Devolver respuesta
            return new PedidoResponse
            {
                Id = pedido.Id,
                EstatusGeneral = pedido.EstatusGeneral,
                Total = pedido.Total,
                FechaUltimaModificacion = pedido.FechaUltimaModificacion,
                FkUsuario = pedido.FkUsuario,
                FkEmpresa = pedido.FkEmpresa
            };
        }
    }
}
