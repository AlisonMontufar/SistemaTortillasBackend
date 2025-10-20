using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Application.Dtos.Order;
using MediatR;

namespace Tortillas.Application.UseCases.Order
{
    public class CreatePedidoHandler : IRequestHandler<CreatePedidoRequest, int>
    {
        private readonly IPedidoRepository _pedidoRepo;
        private readonly IDetallePedidoRepository _detalleRepo;

        public CreatePedidoHandler(IPedidoRepository pedidoRepo, IDetallePedidoRepository detalleRepo)
        {
            _pedidoRepo = pedidoRepo;
            _detalleRepo = detalleRepo;
        }

        public async Task<int> Handle(CreatePedidoRequest request, CancellationToken cancellationToken)
        {
            // Construir pedido con detalles y pago
            var pedido = new Pedido
            {
                FkEmpresa = request.FkEmpresa,
                FkUsuario = request.FkUsuario,
                FkDireccion = request.FkDireccion,
                FechaEntrega = request.FechaEntrega,
                Total = request.Total,
                FechaUltimaModificacion = DateTime.UtcNow,
                Detalles = request.Detalles.Select(d => new DetallePedido
                {
                    FkDireccion = request.FkDireccion,
                    ProductoNombre = d.ProductoNombre,
                    Cantidad = d.Cantidad,
                    NombreSucursal = d.NombreSucursal ?? string.Empty,
                    EstatusNombre = d.EstatusNombre ?? "Pendiente",
                    FechaHora = d.FechaHora == default ? DateTime.UtcNow : d.FechaHora,
                    FechaUltimaModificacion = DateTime.UtcNow
                }).ToList(),
                Pago = request.Pago == null ? null : new Pago
                {
                    NombreTitular = request.Pago.NombreTitular,
                    MetodoPago = request.Pago.MetodoPago,
                    NumeroEnmascarado = request.Pago.NumeroEnmascarado,
                    MarcaTarjeta = request.Pago.MarcaTarjeta,
                    ExpMes = request.Pago.ExpMes,
                    ExpAnio = request.Pago.ExpAnio,
                    FechaRegistro = DateTime.UtcNow
                    
                }

            };

            // Guardar todo en EF Core (pedido + detalles + pago)
            int pedidoId = await _pedidoRepo.CreatePedidoAsync(pedido);

            // Retornar el Id generado
            return pedidoId;
        }
    }
}
