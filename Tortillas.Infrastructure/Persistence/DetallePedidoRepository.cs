using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Infrastructure.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace Tortillas.Infrastructure.Persistence
{
    public class DetallePedidoRepository : IDetallePedidoRepository
    {
        private readonly TortillasDbContext _context;

        public DetallePedidoRepository(TortillasDbContext context)
        {
            _context = context;
        }

        // Obtener detalle por Id (puede ser null si no existe)
        public async Task<DetallePedido?> GetDetalleByIdAsync(int id)
        {
            return await _context.DetallePedido.FirstOrDefaultAsync(d => d.Id == id);
        }

        // Obtener todos los detalles de un pedido
        public async Task<IEnumerable<DetallePedido>> GetAllDetallesAsync(int pedidoId)
        {
            return await _context.DetallePedido
                .Where(d => d.FkPedido == pedidoId)
                .ToListAsync();
        }

        // Crear detalle y devolver Id generado
        public async Task CreateDetalleAsync(DetallePedido detalle)
        {
            _context.DetallePedido.Add(detalle);
            await _context.SaveChangesAsync();
        }

        // Actualizar detalle
        public async Task UpdateDetalleAsync(DetallePedido detalle)
        {
            _context.DetallePedido.Update(detalle);
            await _context.SaveChangesAsync();
        }

        // Eliminar detalle
        public async Task DeleteDetalleAsync(int id)
        {
            var detalle = await _context.DetallePedido.FindAsync(id);
            if (detalle != null)
            {
                _context.DetallePedido.Remove(detalle);
                await _context.SaveChangesAsync();
            }
        }
    }
}
