using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortilleria.Infrastructure.DataContexts;

namespace Tortilleria.Infrastructure.Persistence
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly TortilleriaDbContext _context;

        public CompanyRepository(TortilleriaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync()
        {
            return await _context.Empresa.ToListAsync(); // usa singular como tu DbSet
        }
    }

}
