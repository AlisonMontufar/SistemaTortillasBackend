using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Address
{
    public class UpdateSucursalResponse
    {
        public int EmpresaId { get; set; }
        public int DireccionId { get; set; }
        public string Mensaje { get; set; }
    }
}
