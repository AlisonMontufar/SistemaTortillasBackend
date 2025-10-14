using System;

namespace Tortillas.Domain.Entities
{
    public class Vehiculo
    {
        public int Id { get; set; }

        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int Anio { get; set; }
        public string Placas { get; set; } = null!;
        public string Color { get; set; } = null!;
        public byte Estatus { get; set; } = 1;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
