using Microsoft.AspNetCore.Mvc;
using Tortillas.Application.UseCases.Address;
using Tortillas.Application.Dtos.Address;

namespace BackTortillas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DireccionController : ControllerBase
    {
        private readonly GetDireccionesHandler _getDireccionesHandler;
        private readonly GetDireccionByIdHandler _getDireccionByIdHandler;
        private readonly CreateDireccionHandler _createDireccionHandler;
        private readonly UpdateDireccionHandler _updateDireccionHandler;
        private readonly DeleteDireccionHandler _deleteDireccionHandler;

        public DireccionController(
            GetDireccionesHandler getDireccionesHandler,
            GetDireccionByIdHandler getDireccionByIdHandler,
            CreateDireccionHandler createDireccionHandler,
            UpdateDireccionHandler updateDireccionHandler,
            DeleteDireccionHandler deleteDireccionHandler)
        {
            _getDireccionesHandler = getDireccionesHandler;
            _getDireccionByIdHandler = getDireccionByIdHandler;
            _createDireccionHandler = createDireccionHandler;
            _updateDireccionHandler = updateDireccionHandler;
            _deleteDireccionHandler = deleteDireccionHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getDireccionesHandler.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getDireccionByIdHandler.Handle(id);
            if (result == null) return NotFound("Dirección no encontrada");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDireccionRequest request)
        {
            var result = await _createDireccionHandler.Handle(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDireccionRequest request)
        {
            request.Id = id;
            var result = await _updateDireccionHandler.Handle(request);
            if (result == null) return NotFound("Dirección no encontrada");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _deleteDireccionHandler.Handle(id);
            if (!success) return NotFound("Dirección no encontrada");
            return NoContent();
        }
    }
}
