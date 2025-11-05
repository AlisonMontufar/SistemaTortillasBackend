using System;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Sucursal;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Domain.Interfaces.Services;

namespace Tortillas.Application.UseCases.Sucursal
{
    public class CreateSucursalHandler
    {
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IDireccionRepository _direccionRepository;
        private readonly INotificationService _notificationService;

        public CreateSucursalHandler(
            ISucursalRepository sucursalRepository,
            IDireccionRepository direccionRepository,
            INotificationService notificationService)
        {
            _sucursalRepository = sucursalRepository;
            _direccionRepository = direccionRepository;
            _notificationService = notificationService;
        }

        public async Task<CreateSucursalResponse> Handle(CreateSucursalRequest request)
        {
            // 1️⃣ Validar que venga la dirección
            if (request.Direccion == null)
                throw new Exception("Debe proporcionarse la dirección de la sucursal.");

            // 2️⃣ Crear dirección primero (usando CreateDireccionAsync)
            var direccion = new Direccion
            {
                Calle = request.Direccion.Calle,
                Numero = request.Direccion.Numero,
                Colonia = request.Direccion.Colonia,
                Ciudad = request.Direccion.Ciudad,
                Estado = request.Direccion.Estado,
                CP = request.Direccion.CP,
                Referencias = request.Direccion.Referencias
            };

            var nuevaDireccion = await _direccionRepository.CreateDireccionAsync(direccion);

            // 3️⃣ Enviar link de registro al encargado
            var sent = await _notificationService.SendRegistrationLinkAsync(request.CorreoElectronico, 2);
            if (!sent)
                throw new Exception("No se pudo enviar el enlace de registro al encargado.");

            // 4️⃣ Crear sucursal asociada a la dirección recién creada
            var sucursal = new Tortillas.Domain.Entities.Sucursal
            {
                NombreSucursal = request.NombreSucursal,
                Telefono = request.Telefono,
                CorreoElectronico = request.CorreoElectronico,
                NombreEncargado = request.NombreEncargado,
                FkDireccion = nuevaDireccion.Id, // 👈 se usa el ID que viene del objeto devuelto
                FkEmpresa = request.FkEmpresa,
                FechaRegistro = DateTime.Now,
                Estatus = 1
            };

            int sucursalId = await _sucursalRepository.AddSucursalAsync(sucursal);

            // 5️⃣ Respuesta final
            return new CreateSucursalResponse
            {
                SucursalId = sucursalId,
                Mensaje = "Sucursal y dirección registradas correctamente. Enlace enviado al encargado."
            };
        }
    }
}
