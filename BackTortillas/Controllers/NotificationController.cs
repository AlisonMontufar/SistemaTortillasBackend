using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Auth;
using Tortillas.Application.Notifications;
using Tortillas.Application.Services;
using Tortillas.Application.UseCases.Auth;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Domain.Interfaces.Services.Auth;
using Tortilleria.Infrastructure.Services.Auth;

namespace Tortillas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly PasswordRecovery _passwordRecovery;
        private readonly UserEmailNotificationService _emailNotification;
        private readonly IUserRepository _userRepo;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly SendRegistrationLink _sendRegistrationLink;

        public NotificationController(
            PasswordRecovery passwordRecovery,
            UserEmailNotificationService emailNotification,
            IUserRepository userRepo,
            IJwtTokenService jwtTokenService,
            SendRegistrationLink sendRegistrationLink)
        {
            _passwordRecovery = passwordRecovery;
            _emailNotification = emailNotification;
            _userRepo = userRepo;
            _jwtTokenService = jwtTokenService;
            _sendRegistrationLink = sendRegistrationLink;
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
        [HttpPost("send-registration-link")]
        public async Task<IActionResult> SendRegistrationLink([FromBody] RegistrationLinkRequest request)
        {
            var result = await _sendRegistrationLink.HandleAsync(request);

            if (!result)
                return BadRequest(new { message = "No se pudo enviar el correo." });

            return Ok(new { message = "Correo enviado correctamente." });
        }

    }
}
