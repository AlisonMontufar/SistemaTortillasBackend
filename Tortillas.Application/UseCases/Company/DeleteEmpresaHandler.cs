using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Company
{
    public class DeleteEmpresaHandler : IRequestHandler<DeleteEmpresaCommand>
    {
        private readonly IEmpresaRepository _empresaRepository;

        public DeleteEmpresaHandler(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<Unit> Handle(DeleteEmpresaCommand request, CancellationToken cancellationToken)
        {
            await _empresaRepository.DeleteEmpresaAsync(request.Id);
            return Unit.Value;
        }

        Task IRequestHandler<DeleteEmpresaCommand>.Handle(DeleteEmpresaCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
