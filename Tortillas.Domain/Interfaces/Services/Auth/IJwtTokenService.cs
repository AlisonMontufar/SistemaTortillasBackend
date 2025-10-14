using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Domain.Interfaces.Services.Auth
{
    public interface IJwtTokenService
    {
        string GenerateToken(string username, int roleId, DateTime? expires = null);
    }
}
