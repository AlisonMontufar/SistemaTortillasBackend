using System.ComponentModel.DataAnnotations;

namespace Tortillas.Application.Dtos.Auth
{
    public class LoginRequest
    {

        public string Identificador { get; set; } = string.Empty;
        public string ContrasenaUsuario { get; set; } = string.Empty;

    }
}