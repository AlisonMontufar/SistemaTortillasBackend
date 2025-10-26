using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;

namespace Tortillas.Domain.Interfaces.Repositories
{
    public interface IDireccionRepository
    {
        Task<List<Direccion>> GetDireccionesAsync();
        Task<Direccion> GetDireccionByIdAsync(int id);
        Task<Direccion> CreateDireccionAsync(Direccion direccion);
        Task<Direccion> UpdateDireccionAsync(Direccion direccion);
        Task<bool> DeleteDireccionAsync(int id);
    }

}
