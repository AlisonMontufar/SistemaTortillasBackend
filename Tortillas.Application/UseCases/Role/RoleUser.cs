using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Rol;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application
{
    public class RoleUser
    {
        private readonly IRoleRepository _roleRepository;

        public RoleUser(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleResponse>> ExecuteAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            return roles.Select(r => new RoleResponse
            {
                Id = r.Id,
                NombreRol = r.NombreRol,
                DescripcionRol = r.DescripcionRol
            });
        }
    }
}