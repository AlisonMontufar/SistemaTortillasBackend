using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Auth;
using Tortillas.Application.Notifications;
using Tortillas.Application.Services;
using Tortillas.Application.UseCases.Auth;

namespace Tortillas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly PasswordRecovery _passwordRecovery;
        private readonly UserEmailNotificationService _emailNotification;

        public NotificationController(
            PasswordRecovery passwordRecovery,
            UserEmailNotificationService emailNotification)
        {
            _passwordRecovery = passwordRecovery;
            _emailNotification = emailNotification;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] NotificationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Enum.IsDefined(typeof(NotificationType), request.Type))
                return BadRequest(new { message = "Tipo de notificación no válido." });

            string code = null;

            if (request.Type == NotificationType.PasswordRecovery)
            {
                var (success, generatedCode) = await _passwordRecovery.GenerateRecoveryCodeAsync(request.Email);
                if (!success)
                    return BadRequest(new { message = "Correo no registrado" });

                code = generatedCode;
            }

            var sent = await _emailNotification.SendNotificationAsync(request.Email, request.Type, code);
            if (!sent)
                return BadRequest(new { message = "No se pudo enviar el mensaje." });

            return Ok(new { message = "Mensaje enviado correctamente" });
        }
    }
}
