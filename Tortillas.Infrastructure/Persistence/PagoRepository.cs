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

        public PagoRepository(TortillasDbContext context) => _context = context;

        public async Task<int> AddPagoAsync(Pago pago)
        {
            _context.Pago.Add(pago);
            await _context.SaveChangesAsync();
            return pago.Id;
        }

        public async Task<Pago?> GetPagoByPedidoAsync(int pedidoId)
        {
            return await _context.Pago
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.FkPedido == pedidoId);
        }
    }
}
