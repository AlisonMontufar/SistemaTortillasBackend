using System.Threading.Tasks;
using Tortillas.Application.Dtos.Sucursal;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Sucursal
{
    public class GetSucursalByIdHandler
    {
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IDireccionRepository _direccionRepository; // ✅ agregado

        public GetSucursalByIdHandler(
            ISucursalRepository sucursalRepository,
            IEmpresaRepository empresaRepository,
            IDireccionRepository direccionRepository) // ✅ agregado
        {
            _sucursalRepository = sucursalRepository;
            _empresaRepository = empresaRepository;
            _direccionRepository = direccionRepository;
        }

        public async Task<GetSucursalesResponse?> Handle(GetSucursalByIdRequest request)
        {
            var sucursal = await _sucursalRepository.GetSucursalByIdAsync(request.SucursalId);
            if (sucursal == null) return null;

            var empresa = await _empresaRepository.GetEmpresaByIdAsync(sucursal.FkEmpresa);
            var direccion = await _direccionRepository.GetDireccionByIdAsync(sucursal.FkDireccion); // ✅ agregado

            return new GetSucursalesResponse
            {
                SucursalId = sucursal.Id,
                NombreSucursal = sucursal.NombreSucursal,
                Estatus = sucursal.Estatus,
                CorreoElectronico = sucursal.CorreoElectronico,
                Telefono = sucursal.Telefono,
                NombreEncargado = sucursal.NombreEncargado,
                FkEmpresa = sucursal.FkEmpresa,
                NombreEmpresa = empresa?.NombreEmpresa,

                Direccion = direccion == null ? null : new DireccionResponse // ✅ agregado
                {
                    Id = direccion.Id,
                    Calle = direccion.Calle,
                    Numero = direccion.Numero,
                    Colonia = direccion.Colonia,
                    Ciudad = direccion.Ciudad,
                    Estado = direccion.Estado,
                    CP = direccion.CP,
                    Latitud = direccion.Latitud,
                    Longitud = direccion.Longitud,
                    Referencias = direccion.Referencias
                }
            };
        }
    }
}
