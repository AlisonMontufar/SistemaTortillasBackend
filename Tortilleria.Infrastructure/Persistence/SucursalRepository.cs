using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortilleria.Infrastructure.DataContexts;

namespace Tortilleria.Infrastructure.Persistence
{
    public class SucursalRepository : ISucursalRepository
    {
        private readonly TortilleriaDbContext _context;

        public SucursalRepository(TortilleriaDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddSucursalAsync(Sucursal sucursal)
        {
            _context.Sucursal.Add(sucursal);
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
                .Include(s => s.Empresa)
                .FirstOrDefaultAsync(s => s.Id == sucursalId);
        }

        public async Task<List<Sucursal>> GetSucursalesByEmpresaAsync(int empresaId)
        {
            return await _context.Sucursal
                .Where(s => s.EmpresaId == empresaId)
                .ToListAsync();
        }

        public async Task<List<Sucursal>> GetSucursalesAsync()
        {
            return await _context.Sucursal.Include(s => s.Empresa).ToListAsync();
        }
    }
}
