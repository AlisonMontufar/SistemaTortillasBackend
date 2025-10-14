using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;

namespace Tortillas.Domain.Interfaces.Repositories
{
    public interface IRoleRepository
    {
 
            Task<IEnumerable<Rol>> GetAllAsync();
    }
}
