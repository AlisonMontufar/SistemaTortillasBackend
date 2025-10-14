using System.Threading.Tasks;
using Tortillas.Domain.Entities;

namespace Tortillas.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<Usuario?> GetByUsernameAsync(string nombreUsuario);
        Task<Usuario?> GetByEmailAsync(string correoUsuario);
        Task<Usuario> AddAsync(Usuario usuario);
        Task<Usuario> UpdateAsync(Usuario usuario);
    }
}
