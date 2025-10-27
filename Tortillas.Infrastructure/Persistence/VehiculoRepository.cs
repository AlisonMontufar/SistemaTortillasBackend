using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortilleria.Infrastructure.DataContexts;

namespace Tortilleria.Infrastructure.Repositories
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly TortilleriaDbContext _context;

        public VehiculoRepository(TortilleriaDbContext context)
        {
            _context = context;
        }

        public async Task<Vehiculo?> GetByIdAsync(int id)
        {
            return await _context.Vehiculo.FindAsync(id);
        }

        public async Task<Vehiculo?> GetByPlacasAsync(string placas)
        {
            return await _context.Vehiculo
                                 .FirstOrDefaultAsync(v => v.Placas == placas);
        }

        public async Task<Vehiculo> AddAsync(Vehiculo vehiculo)
        {
            _context.Vehiculo.Add(vehiculo);
            await _context.SaveChangesAsync();
            return vehiculo;
        }
    }
}
