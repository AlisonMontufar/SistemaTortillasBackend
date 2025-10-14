using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Auth;
using Tortillas.Application.Notifications;
using Tortillas.Application.Services;
using Tortillas.Application.UseCases.Auth;
using Tortillas.Domain.Interfaces.Services.Auth;

namespace Tortillas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly LoginUser _loginUser;
        private readonly RegisterUser _registerUser;
        private readonly PasswordRecovery _passwordRecovery;
        private readonly UserEmailNotificationService _emailNotification;
        private readonly IAuthService _authService;

        public NotificationController(
            LoginUser loginUser,
            RegisterUser registerUser,
            PasswordRecovery passwordRecovery,
            UserEmailNotificationService emailNotification,
            IAuthService authService)
        {
            _loginUser = loginUser;
            _registerUser = registerUser;
            _passwordRecovery = passwordRecovery;
            _emailNotification = emailNotification;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Identificador))
                return BadRequest(new { message = "Debe proporcionar un identificador (correo o nombre de usuario)." });

            var response = await _loginUser.HandleAsync(request);
            if (response == null)
                return Unauthorized(new { message = "Credenciales inválidas" });

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = await _registerUser.HandleAsync(request);
            if (user == null)
                return BadRequest(new { message = "Username or email already exists" });

            await _emailNotification.SendNotificationAsync(user.CorreoUsuario, NotificationType.Welcome);
            return Ok(new { message = "Usuario registrado correctamente. Se ha enviado un mensaje de bienvenida." });
        }

        [HttpPost("send-notification")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
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

            return Ok(new { message = "Mensaje enviado al correo" });
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] RecoveryVerifyRequest request)
        {
            var result = await _passwordRecovery.VerifyRecoveryCodeAsync(request.Email, request.Code);
            if (!result)
                return BadRequest(new { message = "Código inválido o expirado" });

            return Ok(new { message = "Código válido" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, [FromQuery] string email)
        {
            if (request.NewPassword != request.ConfirmPassword)
                return BadRequest(new { message = "Las contraseñas no coinciden." });

            var result = await _passwordRecovery.ResetPasswordAsync(email, request.NewPassword, _authService);
            if (!result)
                return BadRequest(new { message = "No se pudo cambiar la contraseña. El código expiró o el usuario no existe." });

            await _emailNotification.SendNotificationAsync(email, NotificationType.PasswordResetConfirmation);
            return Ok(new { message = "Contraseña actualizada correctamente. Se ha enviado una confirmación por correo." });
        }
    }
}