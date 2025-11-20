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
    public class CreateDireccionHandler
    {
        private readonly IDireccionRepository _direccionRepository;

        public CreateDireccionHandler(IDireccionRepository direccionRepository)
        {
            _direccionRepository = direccionRepository;
        }

        public async Task<GetDireccionResponse> Handle(CreateDireccionRequest request)
        {
            var direccion = new Direccion
            {
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

            var created = await _direccionRepository.CreateDireccionAsync(direccion);

            return new GetDireccionResponse
            {
                Id = created.Id,
                Calle = created.Calle,
                Numero = created.Numero,
                Colonia = created.Colonia,
                Ciudad = created.Ciudad,
                Estado = created.Estado,
                CP = created.CP,
                Latitud = created.Latitud,
                Longitud = created.Longitud,
                Referencias = created.Referencias,
                FechaUltimaModificacion = created.FechaUltimaModificacion
            };
        }
    }
}
