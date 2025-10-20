using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;

namespace Tortillas.Domain.Interfaces.Repositories
{
    public interface IAddressRepository
    {
        Task<int> AddDireccionAsync(Direccion direccion);
        Task UpdateDireccionAsync(Direccion direccion);
        Task<Direccion?> GetDireccionByEmpresaIdAsync(int empresaId);
        Task DeleteDireccionAsync(int direccionId);
    }
}
