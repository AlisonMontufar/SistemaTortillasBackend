using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Company
{
    public class CompanyResponse
    {
        public int Id { get; set; }
        public string NombreEmpresa { get; set; }
        public string Telefono { get; set; }
        public string CorreoEmpresa { get; set; }
        public byte Estatus { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
