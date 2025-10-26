using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Address;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Address
{
    public class GetDireccionByIdHandler
    {
        private readonly IDireccionRepository _direccionRepository;

        public GetDireccionByIdHandler(IDireccionRepository direccionRepository)
        {
            _direccionRepository = direccionRepository;
        }

        public async Task<GetDireccionResponse> Handle(int id)
        {
            var d = await _direccionRepository.GetDireccionByIdAsync(id);
            if (d == null) return null;

            return new GetDireccionResponse
            {
                Id = d.Id,
                Calle = d.Calle,
                Numero = d.Numero,
                Colonia = d.Colonia,
                Ciudad = d.Ciudad,
                Estado = d.Estado,
                CP = d.CP,
                Referencias = d.Referencias,
                FechaUltimaModificacion = d.FechaUltimaModificacion
            };
        }
    }
}


