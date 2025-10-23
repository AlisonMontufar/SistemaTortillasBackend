using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Sucursal;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Sucursal
{
    public class UpdateSucursalHandler
    {
        private readonly ISucursalRepository _sucursalRepository;
        private readonly INotificationService _notificationService;

        public UpdateSucursalHandler(ISucursalRepository sucursalRepository,
                                     INotificationService notificationService)
        {
            _sucursalRepository = sucursalRepository;
            _notificationService = notificationService;
        }

        public async Task<UpdateSucursalResponse> Handle(UpdateSucursalRequest request)
        {
            var sucursal = await _sucursalRepository.GetSucursalByIdAsync(request.SucursalId);
            if (sucursal == null)
                throw new Exception("Sucursal no encontrada");

            // Si cambió el encargado, enviar nuevo link
            if (!string.Equals(sucursal.CorreoElectronico, request.CorreoElectronico, StringComparison.OrdinalIgnoreCase))
            {
                var sent = await _notificationService.SendRegistrationLinkAsync(request.CorreoElectronico, 2);
                if (!sent)
                    throw new Exception("No se pudo enviar el enlace al nuevo encargado.");
                sucursal.CorreoElectronico = request.CorreoElectronico;
            }

            sucursal.NombreSucursal = request.NombreSucursal;
            sucursal.Telefono = request.Telefono;
            sucursal.NombreEncargado = request.NombreEncargado;
            sucursal.Estatus = request.Estatus;
            sucursal.FkEmpresa = request.FkEmpresa;

            await _sucursalRepository.UpdateSucursalAsync(sucursal);

            return new UpdateSucursalResponse
            {
                SucursalId = sucursal.Id,
                Mensaje = "Sucursal actualizada correctamente"
            };
        }
    }


}
