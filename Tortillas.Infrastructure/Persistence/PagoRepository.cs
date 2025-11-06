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
        public class PagoRepository : IPagoRepository
        {
            private readonly TortillasDbContext _context;

            public PagoRepository(TortillasDbContext context)
            {
                _context = context;
            }

            public async Task<Pago> AddAsync(Pago pago)
            {
                _context.Pago.Add(pago);
                await _context.SaveChangesAsync();
                return pago;
            }

            public async Task<Pago?> GetByPedidoIdAsync(int pedidoId)
            {
                return await _context.Pago.FirstOrDefaultAsync(p => p.FkPedido == pedidoId);
            }

            public async Task<Pago?> GetByIdAsync(int id)
            {
                return await _context.Pago.FirstOrDefaultAsync(p => p.Id == id);
            }

            public async Task UpdateAsync(Pago pago)
            {
                _context.Pago.Update(pago);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var pago = await _context.Pago.FindAsync(id);
                if (pago != null)
                {
                    _context.Pago.Remove(pago);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }



