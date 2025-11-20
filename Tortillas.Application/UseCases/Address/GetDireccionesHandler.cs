using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Address;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Address
{
    public class GetDireccionesHandler
    {
        private readonly IDireccionRepository _direccionRepository;

        public GetDireccionesHandler(IDireccionRepository direccionRepository)
        {
            _direccionRepository = direccionRepository;
        }

        public async Task<List<GetDireccionResponse>> Handle()
        {
            var direcciones = await _direccionRepository.GetDireccionesAsync();
            return direcciones.Select(d => new GetDireccionResponse
            {
                Id = d.Id,
                Calle = d.Calle,
                Numero = d.Numero,
                Colonia = d.Colonia,
                Ciudad = d.Ciudad,
                Estado = d.Estado,
                Latitud = d.Latitud,
                Longitud = d.Longitud,
                CP = d.CP,
                Referencias = d.Referencias,
                FechaUltimaModificacion = d.FechaUltimaModificacion
            }).ToList();
        }
    }
}
