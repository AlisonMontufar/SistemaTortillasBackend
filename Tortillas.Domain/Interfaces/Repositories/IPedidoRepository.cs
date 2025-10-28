using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;


namespace Tortillas.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        Task<Pedido?> GetPedidoByIdAsync(int id);                // puede devolver null
        Task<IEnumerable<Pedido>> GetAllPedidosAsync();         // todos los pedidos
        Task<int> CreatePedidoAsync(Pedido pedido);            // devuelve Id generado
        Task UpdatePedidoAsync(Pedido pedido);                 // actualizar pedido
        Task DeletePedidoAsync(int pedidoId);                  // eliminar pedido
        Task SavePagoAsync(int pedidoId, Pago pago);
        Task<IEnumerable<PedidoDetalleEmpresa>> GetPedidosByEmpresaAsync(int idEmpresa);


    }
}
