using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Sucursal
{
    public class UpdateSucursalRequest
    {
        public int SucursalId { get; set; }
        public string NombreSucursal { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string NombreEncargado { get; set; } = string.Empty;
        public int FkEmpresa { get; set; }
        public byte Estatus { get; set; }
    }
}
