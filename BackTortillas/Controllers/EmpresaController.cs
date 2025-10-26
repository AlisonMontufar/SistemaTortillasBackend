using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tortillas.Application.Dtos.Company;
using Tortillas.Application.UseCases.Company;
namespace BackTortillas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmpresaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmpresa(CreateEmpresaDto dto)
        {
            var id = await _mediator.Send(new CreateEmpresaCommand(dto));
            return Ok(new { message = "Empresa creada correctamente", id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpresa(int id, EmpresaDto dto)
        {
            if (id != dto.Id) return BadRequest("Id inconsistente");
            await _mediator.Send(new UpdateEmpresaCommand(dto));
            return Ok("Empresa actualizada correctamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            await _mediator.Send(new DeleteEmpresaCommand(id));
            return Ok("Empresa eliminada correctamente");
        }

        [HttpGet]
        public async Task<IActionResult> GetEmpresas()
        {
            var empresas = await _mediator.Send(new GetEmpresasQuery());
            return Ok(empresas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpresaById(int id)
        {
            var empresa = await _mediator.Send(new GetEmpresaByIdQuery(id));
            if (empresa == null) return NotFound("Empresa no encontrada");
            return Ok(empresa);
        }
    }
}
