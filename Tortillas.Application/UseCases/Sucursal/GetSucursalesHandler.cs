using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Sucursal;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Sucursal
{
    public class GetSucursalesHandler
    {
        private readonly ISucursalRepository _sucursalRepository;

        public GetSucursalesHandler(ISucursalRepository sucursalRepository)
        {
            _sucursalRepository = sucursalRepository;
        }

        public async Task<List<GetSucursalResponse>> Handle()
        {
            var sucursales = await _sucursalRepository.GetSucursalesAsync();

            return sucursales.Select(s => new GetSucursalResponse
            {
                SucursalId = s.Id,
                NombreSucursal = s.NombreSucursal,
                EmailEncargado = s.CorreoElectronico,
                Telefono = s.Telefono,
                NombreEncargado = s.NombreEncargado,
                EmpresaId = s.EmpresaId,
                EmpresaNombre = s.Empresa?.NombreEmpresa
            }).ToList();
        }
    }


}
