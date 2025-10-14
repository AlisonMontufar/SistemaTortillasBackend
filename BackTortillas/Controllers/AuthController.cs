using Microsoft.AspNetCore.Mvc;
using Tortillas.Application.Dtos.Auth;
using Tortillas.Application.UseCases.Auth;
using Tortillas.Domain.Interfaces.Services.Auth;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoginUser _loginUser;
    private readonly RegisterUser _registerUser;
    private readonly PasswordRecovery _passwordRecovery;
    private readonly IAuthService _authService; 

    public AuthController(
        LoginUser loginUser,
        RegisterUser registerUser,
        PasswordRecovery passwordRecovery,
        IAuthService authService) 
    {
        _loginUser = loginUser;
        _registerUser = registerUser;
        _passwordRecovery = passwordRecovery;
        _authService = authService; 
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Identificador))
        {
            return BadRequest(new { message = "Debe proporcionar un identificador (correo o nombre de usuario)." });
        }

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

        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("send-recovery-code")]
    public async Task<IActionResult> SendRecoveryCode([FromBody] RecoveryCodeRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _passwordRecovery.SendRecoveryCodeAsync(request.Email);
        if (!result)
            return BadRequest(new { message = "Correo no registrado" });

        return Ok(new { message = "Código enviado al correo" });
    }

    [HttpPost("verify-recovery-code")]
    public async Task<IActionResult> VerifyRecoveryCode([FromBody] RecoveryVerifyRequest request)
    {
        var result = await _passwordRecovery.VerifyRecoveryCodeAsync(request.Email, request.Code);
        if (!result) return BadRequest(new { message = "Código inválido o expirado" });

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

        return Ok(new { message = "Contraseña actualizada correctamente" });
    }

}
