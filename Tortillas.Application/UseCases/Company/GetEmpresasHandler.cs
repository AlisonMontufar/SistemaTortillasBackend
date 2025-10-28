using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Company;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Infrastructure.DataContexts; // Para acceder al DbContext si quieres contar pedidos

namespace Tortillas.Application.UseCases.Company
{
    public class GetEmpresasHandler : IRequestHandler<GetEmpresasQuery, List<EmpresaDto>>

    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly TortillasDbContext _context; // Para contar los pedidos

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
                NumeroPedidos = (from p in _context.Pedido
                                 join s in _context.Sucursal on p.FkSucursal equals s.Id
                                 where s.FkEmpresa == e.Id
                                 select p).Count()
            }).ToList();

            return empresasDto;
        }

    }
}
