using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Application.Dtos.Order;

namespace Tortillas.Application.UseCases.Order
{
    public class GetPedidoByIdHandler
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IDetallePedidoRepository _detalleRepository;
        private readonly IDetallePedidoSucursalRepository _detalleSucursalRepository;
        private readonly IPagoRepository _pagoRepository;

        public GetPedidoByIdHandler(
            IPedidoRepository pedidoRepository,
            IDetallePedidoRepository detalleRepository,
            IDetallePedidoSucursalRepository detalleSucursalRepository,
            IPagoRepository pagoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _detalleRepository = detalleRepository;
            _detalleSucursalRepository = detalleSucursalRepository;
            _pagoRepository = pagoRepository;
        }

        public async Task<PedidoResponse?> Handle(int pedidoId)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);
            if (pedido == null) return null;

            var detalles = await _detalleRepository.GetByPedidoIdAsync(pedido.Id);
            var pago = await _pagoRepository.GetByPedidoIdAsync(pedido.Id);

            var detallesResponse = await Task.WhenAll(detalles.Select(async d => new DetallePedidoResponse
            {
                Id = d.Id,
                ProductoNombre = d.ProductoNombre,
                Cantidad = d.Cantidad,
                EstatusNombre = d.EstatusNombre,
                SucursalesAsignadas = (await _detalleSucursalRepository.GetByDetalleIdAsync(d.Id))
                    .Select(s => s.FkSucursal)
                    .ToList()
            }));

            return new PedidoResponse
            {
                Id = pedido.Id,
                EstatusGeneral = pedido.EstatusGeneral,
                Total = pedido.Total, 
                FechaUltimaModificacion = pedido.FechaUltimaModificacion,
                FkUsuario = pedido.FkUsuario,
                FkEmpresa = pedido.FkEmpresa,
                Detalles = detallesResponse.ToList(),
                Pago = pago == null ? null : new PagoResponse
                {
                    Id = pago.Id,
                    MetodoPago = pago.MetodoPago,
                    NumeroEnmascarado = pago.NumeroEnmascarado,
                    MarcaTarjeta = pago.MarcaTarjeta,
                    NombreTitular = pago.NombreTitular,
                    FechaRegistro = pago.FechaRegistro
                }
            };
        }
    }
}
