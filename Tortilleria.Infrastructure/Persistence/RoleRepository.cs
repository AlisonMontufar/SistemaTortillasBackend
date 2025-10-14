using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortilleria.Infrastructure.DataContexts;

namespace Tortilleria.Infrastructure.Persistence
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TortilleriaDbContext _context;

        public RoleRepository(TortilleriaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> GetAllAsync()
        {
            return await _context.Rol
                .Where(r => r.Estatus == 1)
                .ToListAsync();
        }
    }
}