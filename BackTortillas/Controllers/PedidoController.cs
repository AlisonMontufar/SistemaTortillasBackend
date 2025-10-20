using Microsoft.AspNetCore.Mvc;
using Tortillas.Application.Dtos.Order;
using Tortillas.Application.UseCases.Order;

namespace BackTortillas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly CreatePedidoHandler _create;
        private readonly GetPedidoHandler _get;
        private readonly ListPedidosHandler _list;
        private readonly UpdatePedidoHandler _update;
        private readonly DeletePedidoHandler _delete;

        public PedidoController(CreatePedidoHandler create, GetPedidoHandler get, ListPedidosHandler list,
                                UpdatePedidoHandler update, DeletePedidoHandler delete)
        {
            _create = create;
            _get = get;
            _list = list;
            _update = update;
            _delete = delete;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePedidoRequest request, CancellationToken cancellationToken)
        {
            var result = await _create.Handle(request, cancellationToken);
            return Ok(result);
        }

        // GET pedido por Id
        [HttpGet("{pedidoId}")]
        public async Task<IActionResult> Get(int pedidoId, CancellationToken cancellationToken)
        {
            var request = new GetPedidoRequest { PedidoId = pedidoId };
            var result = await _get.Handle(request, cancellationToken);
            return Ok(result);
        }

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

    }

}
