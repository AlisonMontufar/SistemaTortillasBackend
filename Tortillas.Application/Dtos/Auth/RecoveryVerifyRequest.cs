using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Dtos.Auth
{
    public class RecoveryVerifyRequest
    {
        public string Email { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
