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

        public async Task<bool> SendNotificationAsync(string email, NotificationType type, string code = null, string token = null)
        {
            string subject;
            string body;

            switch (type)
            {
                case NotificationType.Welcome:
                case NotificationType.PasswordRecovery:
                case NotificationType.PasswordResetConfirmation:
                case NotificationType.AccountBlocked:
                case NotificationType.PedidoConfirmado:
                    // Solo para estos tipos buscamos usuario
                    var user = await _userRepo.GetByEmailAsync(email);
                    if (user == null) return false;

                    if (type == NotificationType.Welcome)
                    {
                        subject = "Bienvenido a Tortillas";
                        body = EmailTemplates.GetWelcomeBody(user.Nombre);
                    }
                    else if (type == NotificationType.PasswordRecovery)
                    {
                        subject = "Recuperar contraseña";
                        body = EmailTemplates.GetRecoveryBody(user.Nombre, code);
                    }
                    else if (type == NotificationType.PasswordResetConfirmation)
                    {
                        subject = "Contraseña actualizada";
                        body = EmailTemplates.GetPasswordResetConfirmation(user.Nombre);
                    }
                    else if (type == NotificationType.AccountBlocked)
                    {
                        subject = "Cuenta bloqueada";
                        body = EmailTemplates.GetAccountBlockedBody(user.Nombre);
                    }
                    else // PedidoConfirmado
                    {
                        subject = "Pedido confirmado";
                        body = EmailTemplates.GetPedidoConfirmadoBody(user.Nombre);
                    }
                    break;

                case NotificationType.RegistrationLink:
                    // Para el link de registro no buscamos usuario
                    subject = "Invitación para registrarte en Tortillas";
                    body = EmailTemplates.GetRegistrationLinkBody(email.Split('@')[0], token);
                    break;

                default:
                    return false;
            }

            await _emailService.SendEmailAsync(email, subject, body);
            return true;
        }
    }
}
