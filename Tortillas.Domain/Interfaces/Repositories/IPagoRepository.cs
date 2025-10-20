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
        Task<int> AddPagoAsync(Pago pago);
        Task<Pago?> GetPagoByPedidoIdAsync(int pedidoId);
        Task DeletePagoByPedidoIdAsync(int pedidoId);
    }
}
