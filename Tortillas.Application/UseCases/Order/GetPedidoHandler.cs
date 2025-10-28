using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Order;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Domain.Entities;
using MediatR;



namespace Tortillas.Application.UseCases.Order
{
    public class GetPedidoHandler : IRequestHandler<GetPedidoRequest, PedidoDto>
    {
        private readonly IPedidoRepository _pedidoRepo;
        private readonly IDetallePedidoRepository _detalleRepo;

        public GetPedidoHandler(IPedidoRepository pedidoRepo, IDetallePedidoRepository detalleRepo)
        {
            _pedidoRepo = pedidoRepo;
            _detalleRepo = detalleRepo;
        }

        public async Task<PedidoDto> Handle(GetPedidoRequest request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepo.GetPedidoByIdAsync(request.PedidoId);
            if (pedido == null) return null!;

            var detalles = await _detalleRepo.GetAllDetallesAsync(pedido.Id);

            return new PedidoDto
            {
                Id = pedido.Id,
                FkSucursal = pedido.FkSucursal,
                FkUsuario = pedido.FkUsuario,
                FkDireccion = pedido.FkDireccion,
                FechaEntrega = pedido.FechaEntrega,
                Total = pedido.Total,
                FechaUltimaModificacion = pedido.FechaUltimaModificacion,
                Detalles = pedido.Detalles?.Select(d => new DetallePedidoDto
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
                }).ToList() ?? new List<DetallePedidoDto>(),
                Pago = pedido.Pago == null ? null : new PagoDto
                {
                    Id = pedido.Pago!.Id,
                    FkPedido = pedido.Pago!.FkPedido,
                    NombreTitular = pedido.Pago!.NombreTitular ?? string.Empty,
                    MetodoPago = pedido.Pago!.MetodoPago,
                    NumeroEnmascarado = pedido.Pago!.NumeroEnmascarado,
                    MarcaTarjeta = pedido.Pago!.MarcaTarjeta,
                    ExpMes = pedido.Pago!.ExpMes ?? 0,
                    ExpAnio = pedido.Pago!.ExpAnio ?? 0,
                    FechaRegistro = pedido.Pago!.FechaRegistro
                }

            };

        }
    }

}
