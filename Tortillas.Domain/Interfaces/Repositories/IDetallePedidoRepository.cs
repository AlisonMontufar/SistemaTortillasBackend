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
        Task<DetallePedido> AddAsync(DetallePedido detalle);
        Task<DetallePedido?> GetByIdAsync(int id);
        Task<List<DetallePedido>> GetByPedidoIdAsync(int pedidoId);
        Task UpdateAsync(DetallePedido detalle);
        Task DeleteAsync(int id);
    }
}
