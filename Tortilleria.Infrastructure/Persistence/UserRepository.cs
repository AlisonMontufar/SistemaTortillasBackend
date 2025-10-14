using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortilleria.Infrastructure.DataContexts;

namespace Tortilleria.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly TortilleriaDbContext _context;

        public UserRepository(TortilleriaDbContext context)
        {
            _context = context;
        }

      
        public async Task<Usuario?> GetByUsernameAsync(string nombreUsuario)
        {
            return await _context.Usuario
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
        }

        public async Task<Usuario?> GetByEmailAsync(string correoUsuario)
        {
            return await _context.Usuario
                .FirstOrDefaultAsync(u => u.CorreoUsuario == correoUsuario);
        }

      
        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
