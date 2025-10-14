using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Auth
{
    public class ResetPasswordRequest
    {
        public string NewPassword { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }


}
