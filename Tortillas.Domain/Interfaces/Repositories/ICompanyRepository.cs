using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;

namespace Tortillas.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task<int> AddEmpresaAsync(Empresa empresa);
        Task UpdateEmpresaAsync(Empresa empresa);
        Task<Empresa?> GetEmpresaByIdAsync(int empresaId);
        Task<List<Empresa>> GetEmpresasAsync();
        Task DeleteEmpresaAsync(int empresaId);
    }
}
