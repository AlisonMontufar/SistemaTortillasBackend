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
        private readonly IDireccionRepository _direccionRepository; // ✅ agregado
        private readonly INotificationService _notificationService;

        public UpdateSucursalHandler(
            ISucursalRepository sucursalRepository,
            IDireccionRepository direccionRepository, // ✅ agregado
            INotificationService notificationService)
        {
            _sucursalRepository = sucursalRepository;
            _direccionRepository = direccionRepository; // ✅ agregado
            _notificationService = notificationService;
        }

        public async Task<UpdateSucursalResponse> Handle(UpdateSucursalRequest request)
        {
            var sucursal = await _sucursalRepository.GetSucursalByIdAsync(request.SucursalId);
            if (sucursal == null)
                throw new Exception("Sucursal no encontrada");

            // ✅ Actualizar correo electrónico y reenviar enlace si cambia
            if (!string.Equals(sucursal.CorreoElectronico, request.CorreoElectronico, StringComparison.OrdinalIgnoreCase))
            {
                var sent = await _notificationService.SendRegistrationLinkAsync(request.CorreoElectronico, 2);
                if (!sent)
                    throw new Exception("No se pudo enviar el enlace al nuevo encargado.");
                sucursal.CorreoElectronico = request.CorreoElectronico;
            }

            // ✅ Actualizar campos de la sucursal
            sucursal.NombreSucursal = request.NombreSucursal;
            sucursal.Telefono = request.Telefono;
            sucursal.NombreEncargado = request.NombreEncargado;
            sucursal.Estatus = request.Estatus;
            sucursal.FkEmpresa = request.FkEmpresa;

            // ✅ Actualizar dirección si viene en el request
            if (request.Direccion != null)
            {
                var direccion = await _direccionRepository.GetDireccionByIdAsync(sucursal.FkDireccion);
                if (direccion != null)
                {
                    direccion.Calle = request.Direccion.Calle;
                    direccion.Numero = request.Direccion.Numero;
                    direccion.Colonia = request.Direccion.Colonia;
                    direccion.Ciudad = request.Direccion.Ciudad;
                    direccion.Estado = request.Direccion.Estado;
                    direccion.CP = request.Direccion.CP;
                    direccion.Latitud = request.Direccion.Latitud;
                    direccion.Longitud = request.Direccion.Longitud;
                    direccion.Referencias = request.Direccion.Referencias;

                    await _direccionRepository.UpdateDireccionAsync(direccion);
                }
            }

            await _sucursalRepository.UpdateSucursalAsync(sucursal);

            return new UpdateSucursalResponse
            {
                SucursalId = sucursal.Id,
                Mensaje = "Sucursal y dirección actualizadas correctamente"
            };
        }
    }
}
