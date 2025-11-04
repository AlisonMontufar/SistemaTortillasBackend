using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Infrastructure.DataContexts;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Tortillas.Infrastructure.Persistence
{
        public class DetallePedidoSucursalRepository : IDetallePedidoSucursalRepository
        {
            private readonly TortillasDbContext _context;

            public DetallePedidoSucursalRepository(TortillasDbContext context)
            {
                _context = context;
            }

            public async Task<DetallePedidoSucursal> AddAsync(DetallePedidoSucursal detalleSucursal)
            {
                _context.DetallePedidoSucursal.Add(detalleSucursal);
                await _context.SaveChangesAsync();
                return detalleSucursal;
            }

            public async Task<List<DetallePedidoSucursal>> GetByDetalleIdAsync(int detalleId)
            {
                return await _context.Set<DetallePedidoSucursal>()
                    .Where(d => d.FkDetallePedido == detalleId)
                    .ToListAsync();
            }

            public async Task DeleteByDetalleIdAsync(int detalleId)
            {
                var registros = await _context.Set<DetallePedidoSucursal>()
                    .Where(d => d.FkDetallePedido == detalleId)
                    .ToListAsync();

                _context.RemoveRange(registros);
                await _context.SaveChangesAsync();
            }
        }
    }


