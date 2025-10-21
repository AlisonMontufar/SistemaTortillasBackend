using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Rol;
using Tortillas.Application.UseCases.Role;

namespace BackTortillas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleUser _roleUser;

        public RoleController(RoleUser roleUser)
        {
            _roleUser = roleUser;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleResponse>>> Get()
        {
            var roles = await _roleUser.ExecuteAsync();
            return Ok(roles);
        }
    }
}