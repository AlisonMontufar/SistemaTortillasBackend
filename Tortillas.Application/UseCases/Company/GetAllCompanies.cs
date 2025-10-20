using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Company;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Domain.Entities;

namespace Tortillas.Application
{
    public class GetAllCompanies
    {
        private readonly ICompanyRepository _companyRepository;

        public GetAllCompanies(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<CompanyResponse>> ExecuteAsync()
        {
            var companies = await _companyRepository.GetAllAsync();

            return companies.Select(c => new CompanyResponse
            {
                Id = c.Id,
                NombreEmpresa = c.NombreEmpresa,
                Telefono = c.Telefono,
                CorreoEmpresa = c.CorreoEmpresa,
                Estatus = c.Estatus,
                FechaRegistro = c.FechaRegistro
            });
        }
    }
}