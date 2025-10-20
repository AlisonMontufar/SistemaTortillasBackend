using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Domain.Interfaces.Services.Auth
{
    public interface IRegistrationLinkService
    {
        string GenerateToken(string email, int roleId, int expirationMinutes = 30);
        bool ValidateToken(string token, out string email, out int roleId);
    }

}
