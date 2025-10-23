using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;

namespace Tortillas.Domain.Interfaces.Repositories
{
    public interface ISucursalRepository
    {
        Task<int> AddSucursalAsync(Sucursal sucursal);
        Task UpdateSucursalAsync(Sucursal sucursal);
        Task DeleteSucursalAsync(int sucursalId);
        Task<Sucursal?> GetSucursalByIdAsync(int sucursalId);
        Task<List<Sucursal>> GetSucursalesByEmpresaAsync(int FkEmpresa);
        Task<List<Sucursal>> GetSucursalesAsync();
    }
}
