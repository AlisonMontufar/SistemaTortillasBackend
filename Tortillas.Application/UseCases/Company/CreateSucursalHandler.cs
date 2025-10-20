using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Address;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;



namespace Tortillas.Application.UseCases.Company
{
    public class CreateSucursalHandler
    {
        private readonly INotificationService _notificationService;
        private readonly ICompanyRepository _empresaRepository;
        private readonly IAddressRepository _direccionRepository;

        public CreateSucursalHandler(INotificationService notificationService,
                                     ICompanyRepository empresaRepository,
                                     IAddressRepository direccionRepository)
        {
            _notificationService = notificationService;
            _empresaRepository = empresaRepository;
            _direccionRepository = direccionRepository;
        }

        public async Task<CreateSucursalResponse> Handle(CreateSucursalRequest request)
        {
            // 1️⃣ Enviar link de registro
            var sent = await _notificationService.SendRegistrationLinkAsync(request.EmailEncargado, 2);
            if (!sent)
                throw new Exception("No se pudo enviar el enlace de registro al encargado.");

            // 2️⃣ Crear empresa (sucursal)
            var empresa = new Empresa
            {
                NombreEmpresa = request.NombreSucursal,
                CorreoEmpresa = request.EmailEncargado,
                Estatus = 1
            };

            int empresaId = await _empresaRepository.AddEmpresaAsync(empresa);

            // 3️⃣ Registrar dirección
            var direccion = new Direccion
            {
                FkEmpresa = empresaId,
                Estado = request.Estado,
                Ciudad = request.Municipio,
                Colonia = request.Colonia,
                Calle = request.Calle,
                Numero = $"{request.NumeroExterior} {request.NumeroInterior}".Trim(),
                CP = request.CodigoPostal,
                Referencias = request.Referencias,
                FechaUltimaModificacion = DateTime.Now
            };

            int direccionId = await _direccionRepository.AddDireccionAsync(direccion);

            return new CreateSucursalResponse
            {
                EmpresaId = empresaId,
                DireccionId = direccionId,
                Mensaje = "Sucursal registrada correctamente y enlace enviado al encargado."
            };
        }
    }
}
