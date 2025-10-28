using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Order;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Order
{
    public class CreatePedidoHandler
    {
        private readonly IPedidoRepository _pedidoRepo;
        private readonly IDetallePedidoRepository _detalleRepo;
        private readonly IPagoRepository _pagoRepo;

        public CreatePedidoHandler(IPedidoRepository pedidoRepo,
                                   IDetallePedidoRepository detalleRepo,
                                   IPagoRepository pagoRepo)
        {
            _pedidoRepo = pedidoRepo;
            _detalleRepo = detalleRepo;
            _pagoRepo = pagoRepo;
        }

        public async Task<CreatePedidoResponse> Handle(CreatePedidoRequest request)
        {
            // 1) Crear entidad Pedido (sin pago)
            var pedido = new Pedido
            {
                FkSucursal = request.FkSucursal,
                FkUsuario = request.FkUsuario,
                FkDireccion = request.FkDireccion,
                FechaEntrega = request.FechaEntrega,
                Total = request.Total,
                FechaUltimaModificacion = DateTime.UtcNow
            };

            // Guardamos sólo pedido para obtener Id (evita conflicto FK circular)
            var pedidoId = await _pedidoRepo.CreatePedidoAsync(pedido);

            // 2) Insertar detalles asociados
            if (request.Detalles != null && request.Detalles.Any())
            {
                foreach (var d in request.Detalles)
                {
                    var detalleEntity = new DetallePedido
                    {
                        FkPedido = pedidoId,
                        FkDireccion = d.FkDireccion,
                        ProductoNombre = d.ProductoNombre,
                        Cantidad = d.Cantidad,
                        EstatusNombre = d.EstatusNombre ?? "Pendiente",
                        FechaHora = d.FechaHora == default ? DateTime.UtcNow : d.FechaHora,
                        NombreSucursal = d.NombreSucursal,
                        FechaUltimaModificacion = DateTime.UtcNow
                    };

                    await _detalleRepo.CreateDetalleAsync(detalleEntity);
                }
            }

            // 3) Si hay pago, insertar pago y actualizar Pedido.PagoId
            if (request.Pago != null)
            {
                var pagoEntity = new Pago
                {
                    FkPedido = pedidoId,
                    MetodoPago = request.Pago.MetodoPago,
                    NumeroEnmascarado = request.Pago.NumeroEnmascarado,
                    MarcaTarjeta = request.Pago.MarcaTarjeta,
                    ExpMes = request.Pago.ExpMes,
                    ExpAnio = request.Pago.ExpAnio,
                    NombreTitular = request.Pago.NombreTitular,
                    TokenPago = request.Pago.TokenPago,
                    FechaRegistro = DateTime.UtcNow
                };

                var pagoId = await _pagoRepo.AddPagoAsync(pagoEntity);

                // Asociar el pago al pedido (actualiza Pedido.PagoId)
                await _pedidoRepo.SetPedidoPagoIdAsync(pedidoId, pagoId);
            }

            return new CreatePedidoResponse { PedidoId = pedidoId };
        }
    }
}
