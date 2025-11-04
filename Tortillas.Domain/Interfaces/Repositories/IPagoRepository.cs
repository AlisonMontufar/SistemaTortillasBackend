using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;

namespace Tortillas.Domain.Interfaces.Repositories
{
    public interface IPagoRepository
    {
        Task<Pago> AddAsync(Pago pago);
        Task<Pago?> GetByIdAsync(int id);
        Task<Pago?> GetByPedidoIdAsync(int pedidoId);
        Task UpdateAsync(Pago pago);
        Task DeleteAsync(int id);
    }
}
