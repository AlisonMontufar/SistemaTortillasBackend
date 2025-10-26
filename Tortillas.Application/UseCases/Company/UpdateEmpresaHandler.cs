using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Company
{
    public class UpdateEmpresaHandler : IRequestHandler<UpdateEmpresaCommand>
    {
        private readonly IEmpresaRepository _empresaRepository;

        public UpdateEmpresaHandler(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<Unit> Handle(UpdateEmpresaCommand request, CancellationToken cancellationToken)
        {
            var empresa = await _empresaRepository.GetEmpresaByIdAsync(request.Empresa.Id);
            if (empresa == null)
                throw new Exception("Empresa no encontrada.");

            empresa.NombreEmpresa = request.Empresa.NombreEmpresa;
            empresa.Logo = request.Empresa.Logo;
            empresa.Estatus = request.Empresa.Estatus;

            await _empresaRepository.UpdateEmpresaAsync(empresa);
            return Unit.Value;
        }

        Task IRequestHandler<UpdateEmpresaCommand>.Handle(UpdateEmpresaCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
