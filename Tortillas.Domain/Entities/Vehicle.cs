using System;

namespace Tortillas.Domain.Entities
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int? Anio { get; set; }
        public string? Placas { get; set; }
        public string? Color { get; set; }
        public byte? Estatus { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
