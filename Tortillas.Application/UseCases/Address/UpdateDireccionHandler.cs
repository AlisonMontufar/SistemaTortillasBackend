using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Address;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Address
{
    public class UpdateDireccionHandler
    {
        private readonly IDireccionRepository _direccionRepository;

        public UpdateDireccionHandler(IDireccionRepository direccionRepository)
        {
            _direccionRepository = direccionRepository;
        }

        public async Task<GetDireccionResponse> Handle(UpdateDireccionRequest request)
        {
            var direccion = new Direccion
            {
                Id = request.Id,
                Calle = request.Calle,
                Numero = request.Numero,
                Colonia = request.Colonia,
                Ciudad = request.Ciudad,
                Estado = request.Estado,
                CP = request.CP,
                Latitud = request.Latitud,
                Longitud = request.Longitud,
                Referencias = request.Referencias
            };

            var updated = await _direccionRepository.UpdateDireccionAsync(direccion);
            if (updated == null) return null;

            return new GetDireccionResponse
            {
                Id = updated.Id,
                Calle = updated.Calle,
                Numero = updated.Numero,
                Colonia = updated.Colonia,
                Ciudad = updated.Ciudad,
                Estado = updated.Estado,
                CP = updated.CP,
                Latitud = updated.Latitud,
                Longitud = updated.Longitud,
                Referencias = updated.Referencias,
                FechaUltimaModificacion = updated.FechaUltimaModificacion
            };
        }
    }
}
