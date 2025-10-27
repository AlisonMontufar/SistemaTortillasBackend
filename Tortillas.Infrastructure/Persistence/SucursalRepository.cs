using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Infrastructure.DataContexts;

namespace Tortillas.Infrastructure.Persistence
{
    public class SucursalRepository : ISucursalRepository
    {
        private readonly TortillasDbContext _context;

        public SucursalRepository(TortillasDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddSucursalAsync(Sucursal sucursal)
        {
            await _context.Sucursal.AddAsync(sucursal);
            await _context.SaveChangesAsync();
            return sucursal.Id;
        }

        public async Task UpdateSucursalAsync(Sucursal sucursal)
        {
            _context.Sucursal.Update(sucursal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSucursalAsync(int sucursalId)
        {
            var sucursal = await _context.Sucursal.FindAsync(sucursalId);
            if (sucursal != null)
            {
                _context.Sucursal.Remove(sucursal);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Sucursal?> GetSucursalByIdAsync(int sucursalId)
        {
            return await _context.Sucursal
                .FirstOrDefaultAsync(s => s.Id == sucursalId);
        }

        public async Task<List<Sucursal>> GetSucursalesAsync()
        {
            return await _context.Sucursal.ToListAsync();
        }

        public async Task<List<Sucursal>> GetSucursalesByEmpresaAsync(int fkEmpresa)
        {
            return await _context.Sucursal
                .Where(s => s.FkEmpresa == fkEmpresa)
                .ToListAsync();
        }
    }
}
