using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Infrastructure.DataContexts;

namespace Tortillas.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly TortillasDbContext _context;

        public UserRepository(TortillasDbContext context)
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
