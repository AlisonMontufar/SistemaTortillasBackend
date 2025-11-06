using System.Collections.Generic;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Sucursal;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Sucursal
{
    public class GetSucursalesByEmpresaHandler
    {
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IDireccionRepository _direccionRepository; // ✅ agregado

        public GetSucursalesByEmpresaHandler(
            ISucursalRepository sucursalRepository,
            IEmpresaRepository empresaRepository,
            IDireccionRepository direccionRepository) // ✅ agregado
        {
            _sucursalRepository = sucursalRepository;
            _empresaRepository = empresaRepository;
            _direccionRepository = direccionRepository;
        }

        public async Task<List<GetSucursalesResponse>> Handle(GetSucursalesByEmpresaRequest request)
        {
            var sucursales = await _sucursalRepository.GetSucursalesByEmpresaAsync(request.FKEmpresa);
            var empresa = await _empresaRepository.GetEmpresaByIdAsync(request.FKEmpresa);

            var response = new List<GetSucursalesResponse>();

            foreach (var s in sucursales)
            {
                var direccion = await _direccionRepository.GetDireccionByIdAsync(s.FkDireccion); // ✅ obtenemos dirección

                response.Add(new GetSucursalesResponse
                {
                    SucursalId = s.Id,
                    NombreSucursal = s.NombreSucursal,
                    Estatus = s.Estatus,
                    CorreoElectronico = s.CorreoElectronico,
                    Telefono = s.Telefono,
                    NombreEncargado = s.NombreEncargado,
                    FkEmpresa = s.FkEmpresa,
                    NombreEmpresa = empresa?.NombreEmpresa,

                    Direccion = direccion == null ? null : new DireccionResponse // ✅ mapeamos dirección
                    {
                        Id = direccion.Id,
                        Calle = direccion.Calle,
                        Numero = direccion.Numero,
                        Colonia = direccion.Colonia,
                        Ciudad = direccion.Ciudad,
                        Estado = direccion.Estado,
                        CP = direccion.CP,
                        Referencias = direccion.Referencias
                    }
                });
            }

            return response;
        }
    }
}
