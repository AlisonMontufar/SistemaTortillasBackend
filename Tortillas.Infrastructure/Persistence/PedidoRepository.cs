using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Infrastructure.DataContexts;

namespace Tortillas.Infrastructure.Persistence
{
        public class PedidoRepository : IPedidoRepository
        {
            private readonly TortillasDbContext _context;

            public PedidoRepository(TortillasDbContext context)
            {
                _context = context;
            }

            public async Task<Pedido> AddAsync(Pedido pedido)
            {
                _context.Pedido.Add(pedido);
                await _context.SaveChangesAsync();
                return pedido;
            }

            public async Task<Pedido?> GetByIdAsync(int id)
            {
                return await _context.Pedido
                    .FirstOrDefaultAsync(p => p.Id == id);
            }

            public async Task<List<Pedido>> GetAllAsync()
            {
                return await _context.Pedido.ToListAsync();
            }

            public async Task UpdateAsync(Pedido pedido)
            {
                _context.Pedido.Update(pedido);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var pedido = await _context.Pedido.FindAsync(id);
                if (pedido != null)
                {
                    _context.Pedido.Remove(pedido);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }



