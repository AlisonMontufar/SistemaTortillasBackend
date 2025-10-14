using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Company
{
    public class GetAllCompanies
    {
        private readonly ICompanyRepository _companyRepository;

        public GetAllCompanies(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<Empresa>> ExecuteAsync()
        {
            return await _companyRepository.GetAllAsync();
        }
    }
}
