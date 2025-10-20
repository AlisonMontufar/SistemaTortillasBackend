using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Address;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Company
{
    public class DeleteSucursalHandler
    {
        private readonly ICompanyRepository _empresaRepository;
        private readonly IAddressRepository _direccionRepository;

        public DeleteSucursalHandler(ICompanyRepository empresaRepository,
                                     IAddressRepository direccionRepository)
        {
            _empresaRepository = empresaRepository;
            _direccionRepository = direccionRepository;
        }

        public async Task<DeleteSucursalResponse> Handle(DeleteSucursalRequest request)
        {
            var direccion = await _direccionRepository.GetDireccionByEmpresaIdAsync(request.EmpresaId);
            if (direccion != null)
                await _direccionRepository.DeleteDireccionAsync(direccion.Id);

            await _empresaRepository.DeleteEmpresaAsync(request.EmpresaId);

            return new DeleteSucursalResponse
            {
                Mensaje = "Sucursal eliminada correctamente"
            };
        }
    }


}
