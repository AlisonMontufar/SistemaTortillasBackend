using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Order;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Order
{
    
        public class GetPedidosByEmpresa
        {
            private readonly IPedidoRepository _repository;

            public GetPedidosByEmpresa(IPedidoRepository repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<DetallePedidoEmpresaDto>> ExecuteAsync(int idEmpresa, CancellationToken cancellationToken = default)
            {
                var raw = await _repository.GetPedidosByEmpresaAsync(idEmpresa);

                return raw.Select(x => new DetallePedidoEmpresaDto
                {
                    IdEmpresa = x.IdEmpresa,
                    NombreEmpresa = x.NombreEmpresa,
                    IdSucursal = x.IdSucursal,
                    NombreSucursal = x.NombreSucursal,
                    Calle = x.Calle,
                    Numero = x.Numero,
                    Colonia = x.Colonia,
                    Ciudad = x.Ciudad,
                    Estado = x.Estado,
                    CP = x.CP,
                    Referencias = x.Referencias,
                    IdPedido = x.IdPedido,
                    Total = x.Total,
                    FechaEntrega = x.FechaEntrega,
                    IdDetalle = x.IdDetalle,
                    ProductoNombre = x.ProductoNombre,
                    Cantidad = (int)x.Cantidad, 
                    EstatusNombre = x.EstatusNombre
                });
            }
        }
    }


