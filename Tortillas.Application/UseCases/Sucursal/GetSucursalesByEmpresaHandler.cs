using System.Collections.Generic;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Sucursal;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Sucursal
{
    public class GetSucursalesByEmpresaHandler
    {
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IEmpresaRepository _empresaRepository;

        public GetSucursalesByEmpresaHandler(
            ISucursalRepository sucursalRepository,
            IEmpresaRepository empresaRepository)
        {
            _sucursalRepository = sucursalRepository;
            _empresaRepository = empresaRepository;
        }

        public async Task<List<GetSucursalResponse>> Handle(GetSucursalesByEmpresaRequest request)
        {
            var sucursales = await _sucursalRepository.GetSucursalesByEmpresaAsync(request.FKEmpresa);
            var empresa = await _empresaRepository.GetEmpresaByIdAsync(request.FKEmpresa);

            var response = new List<GetSucursalResponse>();

            foreach (var s in sucursales)
            {
                response.Add(new GetSucursalResponse
                {
                    SucursalId = s.Id,
                    NombreSucursal = s.NombreSucursal,
                    CorreoElectronico = s.CorreoElectronico,
                    Telefono = s.Telefono,
                    NombreEncargado = s.NombreEncargado,
                    FkEmpresa = s.FkEmpresa,
                    NombreEmpresa = empresa?.NombreEmpresa // ✅ ahora se llena correctamente
                });
            }

            return response;
        }
    }
}
