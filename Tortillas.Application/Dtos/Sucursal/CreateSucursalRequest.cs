using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Sucursal
{
    public class CreateSucursalRequest
    {
        public string NombreSucursal { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string EmailEncargado { get; set; } = string.Empty;
        public string NombreEncargado { get; set; } = string.Empty;
        public int EmpresaId { get; set; }
    }
}
