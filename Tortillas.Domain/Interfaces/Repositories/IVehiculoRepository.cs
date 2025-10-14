using System.Threading.Tasks;
using Tortillas.Domain.Entities;

namespace Tortillas.Domain.Interfaces.Repositories
{
    public interface IVehiculoRepository
    {
        Task<Vehiculo?> GetByIdAsync(int id);
        Task<Vehiculo?> GetByPlacasAsync(string placas);
        Task<Vehiculo> AddAsync(Vehiculo vehiculo);
       
    }
}
