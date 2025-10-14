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
        Task<IEnumerable<Empresa>> GetAllAsync();
    }
}
