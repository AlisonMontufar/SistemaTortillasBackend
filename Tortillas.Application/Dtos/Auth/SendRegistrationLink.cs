using System.Threading.Tasks;
using Tortillas.Application.Dtos.Auth;
using Tortillas.Application.Notifications;
using Tortillas.Application.Services;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Domain.Interfaces.Services.Auth;

namespace Tortillas.Application.UseCases.Auth
{
    public class SendRegistrationLink
    {
        private readonly IUserRepository _userRepo;
        private readonly IRegistrationLinkService _linkService;
        private readonly UserEmailNotificationService _emailService;

        public SendRegistrationLink(
            IUserRepository userRepo,
            IRegistrationLinkService linkService,
            UserEmailNotificationService emailService)
        {
            _userRepo = userRepo;
            _linkService = linkService;
            _emailService = emailService;
        }

        public async Task<bool> HandleAsync(RegistrationLinkRequest request)
        {
            // Validar rol
            if (request.RoleId != 1 && request.RoleId != 2)
                return false;

            // Validar que el correo no exista
            var existingUser = await _userRepo.GetByEmailAsync(request.Email);
            if (existingUser != null)
                return false;

            // Generar token
            var token = _linkService.GenerateToken(request.Email, request.RoleId);

            // Enviar correo
            return await _emailService.SendNotificationAsync(
                request.Email,
                NotificationType.RegistrationLink,
                null,
                token
            );

        }
    }
}
