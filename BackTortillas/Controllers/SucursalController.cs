using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Sucursal;
using Tortillas.Application.UseCases.Sucursal;

namespace BackTortillas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SucursalController : ControllerBase
    {
        private readonly CreateSucursalHandler _createHandler;
        private readonly UpdateSucursalHandler _updateHandler;
        private readonly DeleteSucursalHandler _deleteHandler;
        private readonly GetSucursalesHandler _getHandler;
        private readonly GetSucursalByIdHandler _getByIdHandler;
        private readonly GetSucursalesByEmpresaHandler _getByEmpresaHandler;

        public SucursalController(
                CreateSucursalHandler createHandler,
                UpdateSucursalHandler updateHandler,
                DeleteSucursalHandler deleteHandler,
                GetSucursalesHandler getHandler,
                GetSucursalByIdHandler getByIdHandler,
                GetSucursalesByEmpresaHandler getByEmpresaHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getHandler = getHandler;
            _getByIdHandler = getByIdHandler;
            _getByEmpresaHandler = getByEmpresaHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSucursalRequest request)
            => Ok(await _createHandler.Handle(request));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSucursalRequest request)
            => Ok(await _updateHandler.Handle(request));

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _getHandler.Handle());

        [HttpDelete("{sucursalId}")]
        public async Task<IActionResult> Delete(int sucursalId)
            => Ok(await _deleteHandler.Handle(new DeleteSucursalRequest { SucursalId = sucursalId }));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSucursalById(int id)
        {
            var sucursal = await _getByIdHandler.Handle(new GetSucursalByIdRequest { SucursalId = id });
            if (sucursal == null) return NotFound("Sucursal no encontrada");
            return Ok(sucursal);
        }

        [HttpGet("empresa/{fkEmpresa}")]
        public async Task<IActionResult> GetSucursalesByEmpresa(int fkEmpresa)
        {
            var sucursales = await _getByEmpresaHandler.Handle(new GetSucursalesByEmpresaRequest { FKEmpresa = fkEmpresa });
            return Ok(sucursales);
        }
    }
}
