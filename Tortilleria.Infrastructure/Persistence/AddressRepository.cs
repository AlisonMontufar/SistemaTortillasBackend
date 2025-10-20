using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Domain.Entities;
using Tortilleria.Infrastructure.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace Tortillas.Infrastructure.Persistence
{
    public class AddressRepository : IAddressRepository
    {
        private readonly TortilleriaDbContext _context;

        public AddressRepository(TortilleriaDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddDireccionAsync(Direccion direccion)
        {
            _context.Direccion.Add(direccion);
            await _context.SaveChangesAsync();
            return direccion.Id;
        }

        public async Task UpdateDireccionAsync(Direccion direccion)
        {
            _context.Direccion.Update(direccion);
            await _context.SaveChangesAsync();
        }

        public async Task<Direccion?> GetDireccionByEmpresaIdAsync(int empresaId)
        {
            return await _context.Direccion.FirstOrDefaultAsync(d => d.FkEmpresa == empresaId);
        }

        public async Task DeleteDireccionAsync(int direccionId)
        {
            var direccion = await _context.Direccion.FirstOrDefaultAsync(d => d.Id == direccionId);
            if (direccion != null)
            {
                _context.Direccion.Remove(direccion);
                await _context.SaveChangesAsync();
            }
        }
    }
}
