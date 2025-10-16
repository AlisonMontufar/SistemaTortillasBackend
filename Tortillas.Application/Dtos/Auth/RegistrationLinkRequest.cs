using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Auth
{
    public class RegistrationLinkRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(1, 2, ErrorMessage = "El rol debe ser 1 (Administrador) o 2 (Encargado).")]
        public int RoleId { get; set; }
    }
}
