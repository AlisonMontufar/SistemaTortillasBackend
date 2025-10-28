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
    public class DireccionRepository : IDireccionRepository
    {
        private readonly TortillasDbContext _context;

        public DireccionRepository(TortillasDbContext context)
        {
            _context = context;
        }

        public async Task<List<Direccion>> GetDireccionesAsync()
        {
            return await _context.Direccion.ToListAsync();
        }

        public async Task<Direccion> GetDireccionByIdAsync(int id)
        {
            return await _context.Direccion.FindAsync(id);
        }

        public async Task<Direccion> CreateDireccionAsync(Direccion direccion)
        {
            direccion.FechaUltimaModificacion = DateTime.Now;
            _context.Direccion.Add(direccion);
            await _context.SaveChangesAsync();
            return direccion;
        }

        public async Task<Direccion> UpdateDireccionAsync(Direccion direccion)
        {
            var existing = await _context.Direccion.FindAsync(direccion.Id);
            if (existing == null) return null;

            existing.Calle = direccion.Calle;
            existing.Numero = direccion.Numero;
            existing.Colonia = direccion.Colonia;
            existing.Ciudad = direccion.Ciudad;
            existing.Estado = direccion.Estado;
            existing.CP = direccion.CP;
            existing.Referencias = direccion.Referencias;
            existing.FechaUltimaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteDireccionAsync(int id)
        {
            var direccion = await _context.Direccion.FindAsync(id);
            if (direccion == null) return false;

            _context.Direccion.Remove(direccion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
