using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Address;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Company
{
    public class GetSucursalesHandler
    {
        private readonly ICompanyRepository _empresaRepository;
        private readonly IAddressRepository _direccionRepository;

        public GetSucursalesHandler(ICompanyRepository empresaRepository,
                                    IAddressRepository direccionRepository)
        {
            _empresaRepository = empresaRepository;
            _direccionRepository = direccionRepository;
        }

        public async Task<List<GetSucursalResponse>> Handle()
        {
            var empresas = await _empresaRepository.GetEmpresasAsync();
            var result = new List<GetSucursalResponse>();

            foreach (var empresa in empresas)
            {
                var direccion = await _direccionRepository.GetDireccionByEmpresaIdAsync(empresa.Id);
                result.Add(new GetSucursalResponse
                {
                    EmpresaId = empresa.Id,
                    NombreSucursal = empresa.NombreEmpresa,
                    EmailEncargado = empresa.CorreoEmpresa, // Nuevo
                    Estado = direccion?.Estado,
                    Municipio = direccion?.Ciudad,
                    Colonia = direccion?.Colonia,
                    Calle = direccion?.Calle,
                    CodigoPostal = direccion?.CP,
                    NumeroInterior = direccion?.Numero?.Split(' ').Skip(1).FirstOrDefault(),
                    NumeroExterior = direccion?.Numero?.Split(' ').FirstOrDefault(),
                    Referencias = direccion?.Referencias
                });
            }

            return result;
        }
    }


}
