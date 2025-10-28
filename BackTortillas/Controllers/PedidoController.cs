using Microsoft.AspNetCore.Mvc;
using Tortillas.Application.Dtos.Order;
using Tortillas.Application.UseCases.Order;

namespace BackTortillas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PedidoController : ControllerBase
    {
<<<<<<< Updated upstream
        private readonly CreatePedidoHandler _create;
        private readonly GetPedidoHandler _get;
        private readonly ListPedidosHandler _list;
        private readonly UpdatePedidoHandler _update;
        private readonly DeletePedidoHandler _delete;
        private readonly GetPedidosByEmpresaHandler _getByEmpresa;
        public PedidoController(CreatePedidoHandler create, GetPedidoHandler get, ListPedidosHandler list,
                                UpdatePedidoHandler update, DeletePedidoHandler delete , GetPedidosByEmpresaHandler getByEmpresa)
        {
            _create = create;
            _get = get;
            _list = list;
            _update = update;
            _delete = delete;
            _getByEmpresa = getByEmpresa;

=======
        private readonly CreatePedidoHandler _createHandler;
        private readonly GetPedidoHandler _getHandler;

        public PedidoController(CreatePedidoHandler createHandler, GetPedidoHandler getHandler)
        {
            _createHandler = createHandler;
            _getHandler = getHandler;
>>>>>>> Stashed changes
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePedidoRequest request)
        {
            var result = await _createHandler.Handle(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _getHandler.Handle(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }
<<<<<<< Updated upstream

        // GET lista de pedidos
        [HttpGet]
        public async Task<IActionResult> List(CancellationToken cancellationToken)
        {
            var request = new ListPedidosRequest(); // si no requiere parámetros, se crea vacío
            var result = await _list.Handle(request, cancellationToken);
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePedidoRequest request, CancellationToken cancellationToken)
        {
            var result = await _update.Handle(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{pedidoId}")]
        public async Task<IActionResult> Delete(int pedidoId, CancellationToken cancellationToken)
        {
            var request = new DeletePedidoRequest { PedidoId = pedidoId };
            var result = await _delete.Handle(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("Empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId, CancellationToken cancellationToken)
        {
            var request = new GetPedidosByEmpresaRequest { EmpresaId = empresaId };
            var result = await _getByEmpresa.Handle(request, cancellationToken);
            return Ok(result);
        }

=======
>>>>>>> Stashed changes
    }
}
