

namespace Tortillas.Application.Dtos.Sucursal
{
    public class GetSucursalesResponse
    {
        public int SucursalId { get; set; }
        public string NombreSucursal { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? NombreEncargado { get; set; }
        public int FkEmpresa { get; set; }
        public string? NombreEmpresa { get; set; }
        public byte Estatus { get; set; }


        // ✅ Agrega esta propiedad para que el handler funcione
        public DireccionResponse? Direccion { get; set; }
    }
}
