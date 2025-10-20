using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tortillas.Application;
using Tortillas.Application.UseCases.Company;
using Tortillas.Application.Dtos.Address;

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
        public SucursalController(CreateSucursalHandler createHandler,
                              UpdateSucursalHandler updateHandler,
                              DeleteSucursalHandler deleteHandler,
                              GetSucursalesHandler getHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getHandler = getHandler;
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


        [HttpDelete("{empresaId}")]
        public async Task<IActionResult> Delete(int empresaId)
            => Ok(await _deleteHandler.Handle(new DeleteSucursalRequest { EmpresaId = empresaId }));
    }
    }

