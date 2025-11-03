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
        Task<int> AddPagoAsync(Pago pago); // inserta pago y devuelve id
        Task<Pago?> GetPagoByPedidoAsync(int pedidoId);
    }
}
