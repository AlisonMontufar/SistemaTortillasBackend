using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;

namespace Tortillas.Domain.Interfaces.Repositories
{
    public interface IDetallePedidoSucursalRepository
    {
        Task<DetallePedidoSucursal> AddAsync(DetallePedidoSucursal detalleSucursal);
        Task<List<DetallePedidoSucursal>> GetByDetalleIdAsync(int detalleId);
        Task DeleteByDetalleIdAsync(int detalleId);
    }
}
