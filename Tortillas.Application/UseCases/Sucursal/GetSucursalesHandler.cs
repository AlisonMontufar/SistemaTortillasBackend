using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Sucursal;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Sucursal
{
    public class GetSucursalesHandler
    {
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IDireccionRepository _direccionRepository;

        public GetSucursalesHandler(
            ISucursalRepository sucursalRepository,
            IEmpresaRepository empresaRepository,
            IDireccionRepository direccionRepository)
        {
            _sucursalRepository = sucursalRepository;
            _empresaRepository = empresaRepository;
            _direccionRepository = direccionRepository;
        }

        public async Task<List<GetSucursalesResponse>> Handle()
        {
            var sucursales = await _sucursalRepository.GetSucursalesAsync();

            // 🔹 Filtrar solo las sucursales activas
            var sucursalesActivas = sucursales.Where(s => s.Estatus == 1).ToList();

            var response = new List<GetSucursalesResponse>();

            foreach (var s in sucursalesActivas)
            {
                var empresa = await _empresaRepository.GetEmpresaByIdAsync(s.FkEmpresa);
                var direccion = await _direccionRepository.GetDireccionByIdAsync(s.FkDireccion);

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
                    Direccion = direccion == null ? null : new DireccionResponse
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
                });
            }

            return response;
        }
    }
}
