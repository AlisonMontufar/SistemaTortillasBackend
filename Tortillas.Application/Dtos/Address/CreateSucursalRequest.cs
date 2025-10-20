using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Address
{
    public class CreateSucursalRequest
    {
        public string EmailEncargado { get; set; }
        public string NombreEmpresa { get; set; }
        public string NombreSucursal { get; set; }

        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string CodigoPostal { get; set; }
        public string NumeroInterior { get; set; }
        public string NumeroExterior { get; set; }
        public string Referencias { get; set; }
    }
}
