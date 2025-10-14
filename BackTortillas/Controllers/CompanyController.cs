using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Company;
using Tortillas.Application.UseCases.Company;

namespace BackTortillas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly GetAllCompanies _getAllCompanies;

        public CompanyController(GetAllCompanies getAllCompanies)
        {
            _getAllCompanies = getAllCompanies;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _getAllCompanies.ExecuteAsync();
            if (companies == null || !companies.Any())
                return NotFound(new { message = "No se encontraron empresas." });

            return Ok(companies);
        }
    }
}
