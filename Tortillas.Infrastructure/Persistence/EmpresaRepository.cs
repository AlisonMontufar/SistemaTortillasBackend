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
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly TortillasDbContext _context;

        public EmpresaRepository(TortillasDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddEmpresaAsync(Empresa empresa)
        {
            await _context.Empresa.AddAsync(empresa);
            await _context.SaveChangesAsync();
            return empresa.Id;
        }

        public async Task UpdateEmpresaAsync(Empresa empresa)
        {
            _context.Empresa.Update(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmpresaAsync(int empresaId)
        {
            var empresa = await _context.Empresa.FindAsync(empresaId);
            if (empresa != null)
            {
                _context.Empresa.Remove(empresa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Empresa?> GetEmpresaByIdAsync(int id)
        {
            return await _context.Empresa.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Empresa>> GetEmpresasAsync()
        {
            return await _context.Empresa.ToListAsync();
        }
    }
}
