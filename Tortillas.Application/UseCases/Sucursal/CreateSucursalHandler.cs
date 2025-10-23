using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Sucursal;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;



namespace Tortillas.Application.UseCases.Sucursal
{
    public class CreateSucursalHandler
    {
        private readonly ISucursalRepository _sucursalRepository;
        private readonly INotificationService _notificationService;

        public CreateSucursalHandler(ISucursalRepository sucursalRepository,
                                     INotificationService notificationService)
        {
            _sucursalRepository = sucursalRepository;
            _notificationService = notificationService;
        }

        public async Task<CreateSucursalResponse> Handle(CreateSucursalRequest request)
        {
            // 1️⃣ Enviar link de registro al encargado
            var sent = await _notificationService.SendRegistrationLinkAsync(request.CorreoElectronico, 2);
            if (!sent)
                throw new Exception("No se pudo enviar el enlace de registro al encargado.");

            // 2️⃣ Crear sucursal
            var sucursal = new Tortillas.Domain.Entities.Sucursal
            {
                NombreSucursal = request.NombreSucursal,
                Telefono = request.Telefono,
                CorreoElectronico = request.CorreoElectronico,
                NombreEncargado = request.NombreEncargado,
                FkEmpresa = request.FkEmpresa,
                FechaRegistro = DateTime.Now,
                Estatus = 1
            };

            int sucursalId = await _sucursalRepository.AddSucursalAsync(sucursal);

            return new CreateSucursalResponse
            {
                SucursalId = sucursalId,
                Mensaje = "Sucursal registrada correctamente y enlace enviado al encargado."
            };
        }
    }
}
