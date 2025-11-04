using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Order;
using Tortillas.Application.UseCases.Order; // Tus handlers reales

namespace BackTortillas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly CreatePedidoHandler _crearPedidoHandler;
        private readonly GetPedidoByIdHandler _obtenerPedidoPorIdHandler;
        private readonly UpdatePedidoHandler _actualizarPedidoHandler;

        public PedidosController(
            CreatePedidoHandler crearPedidoHandler,
            GetPedidoByIdHandler obtenerPedidoPorIdHandler,
            UpdatePedidoHandler actualizarPedidoHandler
        )
        {
            _crearPedidoHandler = crearPedidoHandler;
            _obtenerPedidoPorIdHandler = obtenerPedidoPorIdHandler;
            _actualizarPedidoHandler = actualizarPedidoHandler;
        }

        // ✅ Crear un pedido completo (pedido + detalles + sucursales + pago)
        [HttpPost("crear")]
        public async Task<IActionResult> CrearPedido([FromBody] CreatePedidoRequest request)
        {
            if (request == null || request.Detalles == null || request.Detalles.Count == 0)
                return BadRequest(new { mensaje = "Debe incluir al menos un detalle de pedido." });

            // El handler devuelve un PedidoResponse
            var pedidoCreado = await _crearPedidoHandler.Handle(request);

            if (pedidoCreado == null)
                return StatusCode(500, new { mensaje = "Error al crear el pedido." });

            return Ok(new
            {
                mensaje = "Pedido creado correctamente.",
                pedido = pedidoCreado
            });
        }

        // ✅ Obtener pedido por ID con detalles y pago
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoById(int id)
        {
            var pedido = await _obtenerPedidoPorIdHandler.Handle(id);

            if (pedido == null)
                return NotFound(new { mensaje = "Pedido no encontrado." });

            return Ok(pedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedido(int id, [FromBody] UpdatePedidoRequest request)
        {
            if (request == null)
                return BadRequest(new { mensaje = "Datos inválidos." });

            request.Id = id;

            // El handler devuelve un PedidoResponse o null
            var pedidoActualizado = await _actualizarPedidoHandler.Handle(request);

            if (pedidoActualizado == null)
                return NotFound(new { mensaje = "No se pudo actualizar el pedido. Verifique que el ID exista." });

            return Ok(new
            {
                mensaje = "Pedido actualizado correctamente.",
                pedido = pedidoActualizado
            });
        }
    }
}
