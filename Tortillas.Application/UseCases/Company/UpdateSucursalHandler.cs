using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Address;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Company
{
    public class UpdateSucursalHandler
    {
        private readonly ICompanyRepository _empresaRepository;
        private readonly IAddressRepository _direccionRepository;
        private readonly INotificationService _notificationService;

        public UpdateSucursalHandler(ICompanyRepository empresaRepository,
                                     IAddressRepository direccionRepository,
                                     INotificationService notificationService)
        {
            _empresaRepository = empresaRepository;
            _direccionRepository = direccionRepository;
            _notificationService = notificationService;
        }

        public async Task<UpdateSucursalResponse> Handle(UpdateSucursalRequest request)
        {
            var empresa = await _empresaRepository.GetEmpresaByIdAsync(request.EmpresaId);
            if (empresa == null) throw new Exception("Sucursal no encontrada");

            // 🔹 Enviar link al nuevo encargado si cambió
            if (!string.Equals(empresa.CorreoEmpresa, request.EmailEncargado, StringComparison.OrdinalIgnoreCase))
            {
                var sent = await _notificationService.SendRegistrationLinkAsync(request.EmailEncargado, 2);
                if (!sent) throw new Exception("No se pudo enviar el enlace de registro al encargado.");
                empresa.CorreoEmpresa = request.EmailEncargado;
            }

            // 🔹 Actualizar empresa
            empresa.NombreEmpresa = request.NombreSucursal;
            await _empresaRepository.UpdateEmpresaAsync(empresa);

            // 🔹 Actualizar dirección
            var direccion = await _direccionRepository.GetDireccionByEmpresaIdAsync(request.EmpresaId);
            if (direccion != null)
            {
                direccion.Estado = request.Estado;
                direccion.Ciudad = request.Municipio;
                direccion.Colonia = request.Colonia;
                direccion.Calle = request.Calle;
                direccion.Numero = $"{request.NumeroExterior} {request.NumeroInterior}".Trim();
                direccion.CP = request.CodigoPostal;
                direccion.Referencias = request.Referencias;
                direccion.FechaUltimaModificacion = DateTime.Now;

                await _direccionRepository.UpdateDireccionAsync(direccion);
            }

            return new UpdateSucursalResponse
            {
                EmpresaId = empresa.Id,
                DireccionId = direccion?.Id ?? 0,
                Mensaje = "Sucursal actualizada correctamente"
            };
        }
    }


}
