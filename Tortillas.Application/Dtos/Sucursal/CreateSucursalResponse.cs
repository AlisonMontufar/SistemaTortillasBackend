using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Sucursal
{
    public class CreateSucursalResponse
    {
        public int SucursalId { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}
