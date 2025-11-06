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

            public async Task<DetallePedido> AddAsync(DetallePedido detalle)
            {
                _context.DetallePedido.Add(detalle);
                await _context.SaveChangesAsync();
                return detalle;
            }

            public async Task<List<DetallePedido>> GetByPedidoIdAsync(int pedidoId)
            {
                return await _context.DetallePedido
                    .Where(d => d.FkPedido == pedidoId)
                    .ToListAsync();
            }

            public async Task<DetallePedido?> GetByIdAsync(int id)
            {
                return await _context.DetallePedido.FirstOrDefaultAsync(d => d.Id == id);
            }

            public async Task UpdateAsync(DetallePedido detalle)
            {
                _context.DetallePedido.Update(detalle);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
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
