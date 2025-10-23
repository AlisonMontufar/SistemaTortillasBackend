using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Sucursal;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Sucursal
{
    public class DeleteSucursalHandler
    {
        private readonly ISucursalRepository _sucursalRepository;

        public DeleteSucursalHandler(ISucursalRepository sucursalRepository)
        {
            _sucursalRepository = sucursalRepository;
        }

        public async Task<DeleteSucursalResponse> Handle(DeleteSucursalRequest request)
        {
            await _sucursalRepository.DeleteSucursalAsync(request.SucursalId);

            return new DeleteSucursalResponse
            {
                Mensaje = "Sucursal eliminada correctamente"
            };
        }
    }

}
