using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;

namespace Tortillas.Domain.Interfaces.Repositories
{
    public interface IDetallePedidoRepository
    {
        Task<int> CreateDetalleAsync(DetallePedido detalle);
        Task<List<DetallePedido>> GetAllDetallesByPedidoAsync(int pedidoId);
    }
}
