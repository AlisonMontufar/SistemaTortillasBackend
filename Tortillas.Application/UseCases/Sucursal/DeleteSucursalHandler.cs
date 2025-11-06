using System;
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
            // 1️⃣ Buscar la sucursal
            var sucursal = await _sucursalRepository.GetSucursalByIdAsync(request.SucursalId);
            if (sucursal == null)
                throw new Exception("La sucursal no existe o ya fue desactivada.");

            // 2️⃣ Cambiar estatus a inactiva
            sucursal.Estatus = 0;

            // 3️⃣ Actualizar en la base de datos
            await _sucursalRepository.UpdateSucursalAsync(sucursal);

            // 4️⃣ Devolver mensaje
            return new DeleteSucursalResponse
            {
                Mensaje = "Sucursal desactivada correctamente."
            };
        }
    }
}
