using System.Collections.Generic;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Sucursal;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Sucursal
{
    public class GetSucursalesHandler
    {
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IEmpresaRepository _empresaRepository;

        public GetSucursalesHandler(
            ISucursalRepository sucursalRepository,
            IEmpresaRepository empresaRepository)
        {
            _sucursalRepository = sucursalRepository;
            _empresaRepository = empresaRepository;
        }

        public async Task<List<GetSucursalResponse>> Handle()
        {
            var sucursales = await _sucursalRepository.GetSucursalesAsync();
            var response = new List<GetSucursalResponse>();

            foreach (var s in sucursales)
            {
                var empresa = await _empresaRepository.GetEmpresaByIdAsync(s.FkEmpresa);

                response.Add(new GetSucursalResponse
                {
                    SucursalId = s.Id,
                    NombreSucursal = s.NombreSucursal,
                    CorreoElectronico = s.CorreoElectronico,
                    Telefono = s.Telefono,
                    NombreEncargado = s.NombreEncargado,
                    FkEmpresa = s.FkEmpresa,
                    NombreEmpresa = empresa?.NombreEmpresa // ✅ se obtiene desde EmpresaRepository
                });
            }

            return response;
        }
    }
}
