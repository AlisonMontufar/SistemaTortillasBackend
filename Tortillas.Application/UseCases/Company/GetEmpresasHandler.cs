using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Company;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Infrastructure.DataContexts;

namespace Tortillas.Application.UseCases.Company
{
    public class GetEmpresasHandler : IRequestHandler<GetEmpresasQuery, List<EmpresaDto>>
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly TortillasDbContext _context;

        public GetEmpresasHandler(IEmpresaRepository empresaRepository, TortillasDbContext context)
        {
            _empresaRepository = empresaRepository;
            _context = context;
        }

        public async Task<List<EmpresaDto>> Handle(GetEmpresasQuery request, CancellationToken cancellationToken)
        {
            var empresas = await _empresaRepository.GetEmpresasAsync();

            var empresasDto = empresas.Select(e => new EmpresaDto
            {
                Id = e.Id,
                NombreEmpresa = e.NombreEmpresa,
                Logo = e.Logo,
                FechaRegistro = e.FechaRegistro,
                Estatus = e.Estatus,
                // ✅ Contar pedidos directamente con FkEmpresa
                NumeroPedidos = _context.Pedido.Count(p => p.FkEmpresa == e.Id)
            }).ToList();

            return empresasDto;
        }
    }
}
