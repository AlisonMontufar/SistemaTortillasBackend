using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Sucursal
{
    public class GetSucursalResponse
    {
        public int SucursalId { get; set; }
        public string NombreSucursal { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? NombreEncargado { get; set; }
        public int FkEmpresa { get; set; }
    }
}
