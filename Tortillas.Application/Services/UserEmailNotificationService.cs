using System.Threading.Tasks;
using Tortillas.Application.Notifications;
using Tortillas.Application.Templates;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Domain.Interfaces.Services.Auth;


namespace Tortillas.Application.Services
{
    public class UserEmailNotificationService
    {
        private readonly IUserRepository _userRepo;
        private readonly IEmailService _emailService;

        public UserEmailNotificationService(IUserRepository userRepo, IEmailService emailService)
        {
            _userRepo = userRepo;
            _emailService = emailService;
        }

        public async Task<bool> SendNotificationAsync(string email, NotificationType type, string code = null)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user == null) return false;

            string subject;
            string body;

            switch (type)
            {
                case NotificationType.Welcome:
                    subject = "Bienvenido a Tortillas";
                    body = EmailTemplates.GetWelcomeBody(user.Nombre);
                    break;

                case NotificationType.PasswordRecovery:
                    subject = "Recuperar contraseña";
                    body = EmailTemplates.GetRecoveryBody(user.Nombre, code);
                    break;

                case NotificationType.PasswordResetConfirmation:
                    subject = "Contraseña actualizada";
                    body = EmailTemplates.GetPasswordResetConfirmation(user.Nombre);
                    break;

                case NotificationType.AccountBlocked:
                    subject = "Cuenta bloqueada";
                    body = EmailTemplates.GetAccountBlockedBody(user.Nombre);
                    break;

                case NotificationType.PedidoConfirmado:
                    subject = "Pedido confirmado";
                    body = EmailTemplates.GetPedidoConfirmadoBody(user.Nombre);
                    break;

                default:
                    return false;
            }

            await _emailService.SendEmailAsync(email, subject, body);
            return true;
        }
    }
}