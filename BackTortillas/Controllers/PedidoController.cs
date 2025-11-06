using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Order;
using Tortillas.Application.UseCases.Order;
using Tortillas.Domain.Interfaces.Repositories; // Tus handlers reales

namespace BackTortillas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly CreatePedidoHandler _crearPedidoHandler;
        private readonly GetPedidoByIdHandler _obtenerPedidoPorIdHandler;
        private readonly UpdatePedidoHandler _actualizarPedidoHandler;
        private readonly GetPedidosByEmpresaHandler _getByEmpresa;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidosController(
            CreatePedidoHandler crearPedidoHandler,
            GetPedidoByIdHandler obtenerPedidoPorIdHandler,
            UpdatePedidoHandler actualizarPedidoHandler,
            GetPedidosByEmpresaHandler getByEmpresa,
             IPedidoRepository pedidoRepository
        )
        {
            _crearPedidoHandler = crearPedidoHandler;
            _obtenerPedidoPorIdHandler = obtenerPedidoPorIdHandler;
            _actualizarPedidoHandler = actualizarPedidoHandler;
            _getByEmpresa = getByEmpresa;
            _pedidoRepository = pedidoRepository;
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
        [HttpGet("Empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId, CancellationToken cancellationToken)
        {
            var request = new GetPedidosByEmpresaRequest { EmpresaId = empresaId };
            var result = await _getByEmpresa.Handle(request, cancellationToken);
            return Ok(result);
        }

        [HttpPatch("detalle/estatusporpedido")]
        public async Task<IActionResult> ActualizarEstatusPorPedido([FromBody] UpdateEstatusDetallePorPedidoRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EstatusDetalle))
                return BadRequest(new { mensaje = "El estatus no puede estar vacío." });

            var actualizados = await _pedidoRepository.UpdateEstatusDetalleByPedidoIdAsync(request.IdPedido, request.EstatusDetalle);

            if (actualizados == 0)
                return NotFound(new { mensaje = "No se encontraron detalles para el pedido especificado." });

            return Ok(new
            {
                mensaje = "Estatus actualizado correctamente.",
                registrosModificados = actualizados
            });
        }
        [HttpPatch("detalle/firmaporpedido")]
        public async Task<IActionResult> ActualizarFirmaPorPedido([FromBody] UpdateFirmaPorPedidoRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirmaBase64))
                return BadRequest(new { mensaje = "La firma no puede estar vacía." });

            var actualizados = await _pedidoRepository.UpdateFirmaByPedidoIdAsync(request.IdPedido, request.FirmaBase64);

            if (actualizados == 0)
                return NotFound(new { mensaje = "No se encontraron detalles para el pedido especificado." });

            return Ok(new
            {
                mensaje = "Firma actualizada correctamente.",
                registrosModificados = actualizados
            });
        }
    }
}
