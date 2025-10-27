using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Company
{
    public class GetEmpresasHandler : IRequestHandler<GetEmpresasQuery, List<Empresa>>
    {
        private readonly IEmpresaRepository _empresaRepository;

        public GetEmpresasHandler(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<List<Empresa>> Handle(GetEmpresasQuery request, CancellationToken cancellationToken)
        {
            return await _empresaRepository.GetEmpresasAsync();
        }
    }
}
