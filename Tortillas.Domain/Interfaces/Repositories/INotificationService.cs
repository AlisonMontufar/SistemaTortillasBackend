using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Domain.Interfaces.Repositories
{
    public interface INotificationService
    {
        Task<bool> SendRegistrationLinkAsync(string email, int roleId = 2);
    }
}
