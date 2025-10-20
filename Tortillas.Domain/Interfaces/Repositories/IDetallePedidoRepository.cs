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
        Task<DetallePedido?> GetDetalleByIdAsync(int id);                  // puede devolver null
        Task<IEnumerable<DetallePedido>> GetAllDetallesAsync(int pedidoId);// devuelve todos los subpedidos de un pedido
        Task CreateDetalleAsync(DetallePedido detalle);                // crear un subpedido
        Task UpdateDetalleAsync(DetallePedido detalle);                   // actualizar subpedido
        Task DeleteDetalleAsync(int detalleId);
    }
}
