using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Infrastructure.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace Tortillas.Infrastructure.Persistence
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly TortillasDbContext _context;

        public PedidoRepository(TortillasDbContext context)
        {
            _context = context;
        }

        // Obtener pedido por Id
        public async Task<Pedido?> GetPedidoByIdAsync(int pedidoId)
        {
            return await _context.Pedido
                .Include(p => p.Detalles)
                .Include(p => p.Pago)
                .FirstOrDefaultAsync(p => p.Id == pedidoId);
        }

        // Listar todos los pedidos
        public async Task<IEnumerable<Pedido>> GetAllPedidosAsync()
        {
            return await _context.Pedido
                .Include(p => p.Detalles)
                .Include(p => p.Pago)
                .ToListAsync();
        }

        // Crear pedido y devolver Id
        public async Task<int> CreatePedidoAsync(Pedido pedido)
        {
            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido.Id;
        }

        // Actualizar pedido
        public async Task UpdatePedidoAsync(Pedido pedido)
        {
            _context.Pedido.Update(pedido);
            await _context.SaveChangesAsync();
        }

        // Eliminar pedido
        public async Task DeletePedidoAsync(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }

        // Guardar pago de un pedido
        public async Task SavePagoAsync(int pedidoId, Pago pago)
        {
            var pedido = await _context.Pedido
                .Include(p => p.Pago) // Asegúrate de incluir
                .FirstOrDefaultAsync(p => p.Id == pedidoId);

            if (pedido != null)
            {
                pago.FkPedido = pedidoId;

                // Relacionar el pago con el pedido
                pedido.Pago = pago;

                _context.Pago.Add(pago); // Esto agrega a la tabla
                _context.Pedido.Update(pedido); // EF Core ahora sabe la relación
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Pedido no encontrado");
            }
        }

    }
}
