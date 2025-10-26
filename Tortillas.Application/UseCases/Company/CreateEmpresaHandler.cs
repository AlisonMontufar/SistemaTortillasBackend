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
    public class CreateEmpresaHandler : IRequestHandler<CreateEmpresaCommand, int>
    {
        private readonly IEmpresaRepository _empresaRepository;

        public CreateEmpresaHandler(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<int> Handle(CreateEmpresaCommand request, CancellationToken cancellationToken)
        {
            var empresa = new Empresa
            {
                NombreEmpresa = request.Empresa.NombreEmpresa,
                Logo = request.Empresa.Logo,
                FechaRegistro = DateTime.Now,
                Estatus = 1
            };

            return await _empresaRepository.AddEmpresaAsync(empresa);
        }
    }
}
