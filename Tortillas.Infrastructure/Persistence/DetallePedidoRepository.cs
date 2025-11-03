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
        public DetallePedidoRepository(TortillasDbContext context) => _context = context;

        public async Task<int> CreateDetalleAsync(DetallePedido detalle)
        {
            _context.DetallePedido.Add(detalle);
            await _context.SaveChangesAsync();
            return detalle.Id;
        }

        public async Task<List<DetallePedido>> GetAllDetallesByPedidoAsync(int pedidoId)
        {
            return await _context.DetallePedido
                .Where(d => d.FkPedido == pedidoId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
