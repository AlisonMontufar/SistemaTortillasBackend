using System.Threading.Tasks;
using Tortillas.Application.Dtos.Sucursal;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Sucursal
{
    public class GetSucursalByIdHandler
    {
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IEmpresaRepository _empresaRepository;

        public GetSucursalByIdHandler(
            ISucursalRepository sucursalRepository,
            IEmpresaRepository empresaRepository)
        {
            _sucursalRepository = sucursalRepository;
            _empresaRepository = empresaRepository;
        }

        public async Task<GetSucursalResponse?> Handle(GetSucursalByIdRequest request)
        {
            var sucursal = await _sucursalRepository.GetSucursalByIdAsync(request.SucursalId);
            if (sucursal == null) return null;

            var empresa = await _empresaRepository.GetEmpresaByIdAsync(sucursal.FkEmpresa);

            return new GetSucursalResponse
            {
                SucursalId = sucursal.Id,
                NombreSucursal = sucursal.NombreSucursal,
                CorreoElectronico = sucursal.CorreoElectronico,
                Telefono = sucursal.Telefono,
                NombreEncargado = sucursal.NombreEncargado,
                FkEmpresa = sucursal.FkEmpresa,
                NombreEmpresa = empresa?.NombreEmpresa
            };
        }
    }
}
