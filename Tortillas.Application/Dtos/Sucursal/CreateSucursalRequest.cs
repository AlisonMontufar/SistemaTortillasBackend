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
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }  // coincide con la entity
        public string? NombreEncargado { get; set; }
        public int FkEmpresa { get; set; }  // coincide con la entity+
        public int FkDireccion { get; set; }
    }
}
