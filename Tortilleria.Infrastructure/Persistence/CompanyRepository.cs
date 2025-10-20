using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Domain.Entities;
using Tortilleria.Infrastructure.DataContexts;
using Microsoft.EntityFrameworkCore;


namespace Tortillas.Infrastructure.Persistence
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly TortilleriaDbContext _context;

        public CompanyRepository(TortilleriaDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddEmpresaAsync(Empresa empresa)
        {
            _context.Empresa.Add(empresa);
            await _context.SaveChangesAsync();
            return empresa.Id;
        }

        public async Task UpdateEmpresaAsync(Empresa empresa)
        {
            _context.Empresa.Update(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task<Empresa?> GetEmpresaByIdAsync(int empresaId)
        {
            return await _context.Empresa.FirstOrDefaultAsync(e => e.Id == empresaId);
        }

        public async Task<List<Empresa>> GetEmpresasAsync()
        {
            return await _context.Empresa.ToListAsync();
        }

        public async Task DeleteEmpresaAsync(int empresaId)
        {
            var empresa = await _context.Empresa.FirstOrDefaultAsync(e => e.Id == empresaId);
            if (empresa != null)
            {
                _context.Empresa.Remove(empresa);
                await _context.SaveChangesAsync();
            }
        }
    }
}

