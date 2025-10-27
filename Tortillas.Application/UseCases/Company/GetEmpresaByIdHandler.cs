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
    public class GetEmpresaByIdHandler : IRequestHandler<GetEmpresaByIdQuery, Empresa?>
    {
        private readonly IEmpresaRepository _empresaRepository;

        public GetEmpresaByIdHandler(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<Empresa?> Handle(GetEmpresaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _empresaRepository.GetEmpresaByIdAsync(request.Id);
        }
    }
}
