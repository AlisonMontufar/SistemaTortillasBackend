using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Order;
using Tortillas.Application.Dtos.OrderDetails;
using Tortillas.Application.Dtos.Pay;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Order
{
    public class GetPedidoHandler
    {
        private readonly IPedidoRepository _pedidoRepo;
        private readonly IDetallePedidoRepository _detalleRepo;
        private readonly IPagoRepository _pagoRepo;

        public GetPedidoHandler(IPedidoRepository pedidoRepo,
                                IDetallePedidoRepository detalleRepo,
                                IPagoRepository pagoRepo)
        {
            _pedidoRepo = pedidoRepo;
            _detalleRepo = detalleRepo;
            _pagoRepo = pagoRepo;
        }

        public async Task<PedidoDto?> Handle(int pedidoId)
        {
            var pedido = await _pedidoRepo.GetPedidoByIdAsync(pedidoId);
            if (pedido == null) return null;

            var detalles = await _detalleRepo.GetAllDetallesByPedidoAsync(pedidoId);
            var pago = await _pagoRepo.GetPagoByPedidoAsync(pedidoId);

            return new PedidoDto
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
                    FkPedido = d.FkPedido,
                    FkDireccion = d.FkDireccion,
                    ProductoNombre = d.ProductoNombre,
                    Cantidad = d.Cantidad,
                    EstatusNombre = d.EstatusNombre,
                    FechaHora = d.FechaHora,
                    FechaUltimaModificacion = d.FechaUltimaModificacion,
                    NombreSucursal = d.NombreSucursal
                }).ToList(),
                Pago = pago == null ? null : new PagoDto
                {
                    Id = pago.Id,
                    FkPedido = pago.FkPedido,
                    MetodoPago = pago.MetodoPago,
                    NumeroEnmascarado = pago.NumeroEnmascarado,
                    MarcaTarjeta = pago.MarcaTarjeta,
                    ExpMes = pago.ExpMes,
                    ExpAnio = pago.ExpAnio,
                    NombreTitular = pago.NombreTitular,
                    FechaRegistro = pago.FechaRegistro
                }
            };
        }
    }

}
