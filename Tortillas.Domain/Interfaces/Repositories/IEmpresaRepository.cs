using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;


namespace Tortillas.Domain.Interfaces.Repositories
{
    public interface IEmpresaRepository
    {
        Task<int> AddEmpresaAsync(Empresa empresa);
        Task UpdateEmpresaAsync(Empresa empresa);
        Task DeleteEmpresaAsync(int empresaId);
        Task<Empresa?> GetEmpresaByIdAsync(int id);
        Task<List<Empresa>> GetEmpresasAsync();

       
    }
}

