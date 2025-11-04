using System;
using System.Linq;
using System.Threading.Tasks;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Application.Dtos.Order;

namespace Tortillas.Application.UseCases.Order
{
    public class CreatePedidoHandler
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IDetallePedidoRepository _detallePedidoRepository;
        private readonly IDetallePedidoSucursalRepository _detalleSucursalRepository;
        private readonly IPagoRepository _pagoRepository;

        public CreatePedidoHandler(
            IPedidoRepository pedidoRepository,
            IDetallePedidoRepository detallePedidoRepository,
            IDetallePedidoSucursalRepository detalleSucursalRepository,
            IPagoRepository pagoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _detallePedidoRepository = detallePedidoRepository;
            _detalleSucursalRepository = detalleSucursalRepository;
            _pagoRepository = pagoRepository;
        }

        public async Task<CreatePedidoResponse> Handle(CreatePedidoRequest request)
        {
            // Crear pedido
            var pedido = new Domain.Entities.Pedido
            {
                FkUsuario = request.FkUsuario,
                FkEmpresa = request.FkEmpresa,
                Total = request.Total,
                EstatusGeneral = request.EstatusGeneral,
                FechaUltimaModificacion = DateTime.Now
            };
            await _pedidoRepository.AddAsync(pedido);

            // Crear detalles
            var detallesDto = new System.Collections.Generic.List<DetallePedidoDto>();
            foreach (var detalleReq in request.Detalles)
            {
                var detalle = new Domain.Entities.DetallePedido
                {
                    FkPedido = pedido.Id,
                    ProductoNombre = detalleReq.ProductoNombre,
                    Cantidad = detalleReq.Cantidad,
                    EstatusNombre = detalleReq.EstatusNombre,
                    FechaHora = DateTime.Now,
                    FechaUltimaModificacion = DateTime.Now,
                    EstatusDetalle = detalleReq.EstatusDetalle
                };
                await _detallePedidoRepository.AddAsync(detalle);

                // Guardar relaciones con sucursales
                foreach (var sucursalId in detalleReq.SucursalesAsignadas)
                {
                    var relacion = new Domain.Entities.DetallePedidoSucursal
                    {
                        FkDetallePedido = detalle.Id,
                        FkSucursal = sucursalId,
                        FechaAsignacion = DateTime.Now
                    };
                    await _detalleSucursalRepository.AddAsync(relacion);
                }

                // Agregar al DTO
                detallesDto.Add(new DetallePedidoDto
                {
                    Id = detalle.Id,
                    ProductoNombre = detalle.ProductoNombre,
                    Cantidad = detalle.Cantidad,
                    EstatusNombre = detalle.EstatusNombre,
                    EstatusDetalle = detalle.EstatusDetalle,
                    FechaUltimaModificacion = detalle.FechaUltimaModificacion,
                    FechaHora = detalle.FechaHora
                });
            }

            // Crear pago
            var pagoEntity = new Domain.Entities.Pago
            {
                FkPedido = pedido.Id,
                MetodoPago = request.Pago.MetodoPago,
                NumeroEnmascarado = request.Pago.NumeroEnmascarado,
                MarcaTarjeta = request.Pago.MarcaTarjeta,
                ExpMes = request.Pago.ExpMes,
                ExpAnio = request.Pago.ExpAnio,
                NombreTitular = request.Pago.NombreTitular,
                TokenPago = request.Pago.TokenPago,
                FechaRegistro = DateTime.Now
            };
            await _pagoRepository.AddAsync(pagoEntity);

            var pagoDto = new PagoDto
            {
                Id = pagoEntity.Id,
                MetodoPago = pagoEntity.MetodoPago,
                NumeroEnmascarado = pagoEntity.NumeroEnmascarado,
                MarcaTarjeta = pagoEntity.MarcaTarjeta,
                ExpMes = pagoEntity.ExpMes,
                ExpAnio = pagoEntity.ExpAnio,
                NombreTitular = pagoEntity.NombreTitular,
                TokenPago = pagoEntity.TokenPago,
                FechaRegistro = pagoEntity.FechaRegistro
            };

            // Retornar el DTO completo
            return new CreatePedidoResponse
            {
                Id = pedido.Id,
                Total = pedido.Total,
                EstatusGeneral = pedido.EstatusGeneral,
                FechaUltimaModificacion = pedido.FechaUltimaModificacion,
                FkUsuario = pedido.FkUsuario,
                FkEmpresa = pedido.FkEmpresa,
                Detalles = detallesDto,
                Pago = pagoDto
            };
        }
    }
}
