using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Domain.Entities
{
    public class Rol
    {
        public int Id { get; set; }

        public string NombreRol { get; set; }

        public string DescripcionRol { get; set; }

        public byte Estatus { get; set; } = 1;
    }
}