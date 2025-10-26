using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Application.UseCases.Address
{
    public class DeleteDireccionHandler
    {
        private readonly IDireccionRepository _direccionRepository;

        public DeleteDireccionHandler(IDireccionRepository direccionRepository)
        {
            _direccionRepository = direccionRepository;
        }

        public async Task<bool> Handle(int id)
        {
            return await _direccionRepository.DeleteDireccionAsync(id);
        }
    }
}
