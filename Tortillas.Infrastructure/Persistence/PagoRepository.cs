using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Infrastructure.DataContexts;

namespace Tortillas.Infrastructure.Persistence
{
    public class PagoRepository : IPagoRepository
    {
        private readonly TortillasDbContext _context;
        public PagoRepository(TortillasDbContext context) => _context = context;

        public async Task<int> AddPagoAsync(Pago pago)
        {
            await _context.AddAsync(pago);
            await _context.SaveChangesAsync();
            return pago.Id;
        }

        public async Task<Pago?> GetPagoByPedidoIdAsync(int pedidoId)
        {
            return await _context.Set<Pago>().FirstOrDefaultAsync(p => p.FkPedido == pedidoId);
        }

        public async Task DeletePagoByPedidoIdAsync(int pedidoId)
        {
            var pago = await _context.Set<Pago>().FirstOrDefaultAsync(p => p.FkPedido == pedidoId);
            if (pago != null)
            {
                _context.Remove(pago);
                await _context.SaveChangesAsync();
            }
        }
    }

}
