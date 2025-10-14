using System;

namespace Tortillas.Application.Templates
{
    public static class EmailTemplates
    {
        private const string LogoUrl = "https://i.ibb.co/ccZm56TV/Logo.png";

        // HTML base que envuelve cualquier body
        private static string BaseHtml(string bodyContent) =>
            $@"
            <html>
            <body style='font-family: Arial, sans-serif; background-color: #f7f7f7; margin:0; padding:0;'>
                <table width='100%' cellpadding='0' cellspacing='0' style='background-color: #f7f7f7; padding: 20px;'>
                    <tr>
                        <td align='center'>
                            <table width='600' cellpadding='0' cellspacing='0' style='background-color: #ffffff; border-radius: 8px; padding: 20px;'>
                                <tr>
                                    <td align='center' style='padding-bottom: 20px;'>
                                        <img src='{LogoUrl}' alt='Tortillas' width='150' />
                                    </td>
                                </tr>
                                <tr>
                                    <td style='font-size: 16px; color: #333333;'>
                                        {bodyContent}
                                    </td>
                                </tr>
                                <tr>
                                    <td style='padding-top: 30px; font-size: 12px; color: #777777; text-align: center;'>
                                        Este es un correo automático de notificación de Tortillas. Por favor, no respondas a este correo.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";

        public static string GetWelcomeBody(string nombre)
        {
            string body = $@"
                <p>Hola {nombre},</p>
                <p>¡Bienvenido a <b>Tortillas</b>! Tu cuenta ha sido registrada exitosamente.</p>
                <p>Esperamos que disfrutes de nuestros servicios.</p>
                <p>Saludos,<br>El equipo de Tortillas.</p>";

            return BaseHtml(body);
        }

        public static string GetRecoveryBody(string nombre, string code)
        {
            string body = $@"
                <p>Hola {nombre},</p>
                <p>Tu código de recuperación es: <b>{code}</b></p>
                <p>El código expirará en 15 minutos.</p>
                <p>Si no solicitaste este cambio, puedes ignorar este correo.</p>";

            return BaseHtml(body);
        }

        public static string GetPasswordResetConfirmation(string nombre)
        {
            string body = $@"
                <p>Hola {nombre},</p>
                <p>Tu contraseña ha sido restablecida exitosamente.</p>
                <p>Si no realizaste esta acción, por favor contacta al soporte de inmediato.</p>
                <p>Saludos,<br>El equipo de Tortillas.</p>";

            return BaseHtml(body);
        }

        public static string GetAccountBlockedBody(string nombre)
        {
            string body = $@"
                <p>Hola {nombre},</p>
                <p>Tu cuenta ha sido bloqueada por motivos de seguridad.</p>
                <p>Por favor contacta al soporte para más información.</p>
                <p>Saludos,<br>El equipo de Tortillas.</p>";

            return BaseHtml(body);
        }

        public static string GetPedidoConfirmadoBody(string nombre)
        {
            string body = $@"
                <p>Hola {nombre},</p>
                <p>Tu pedido ha sido confirmado y está en proceso.</p>
                <p>Gracias por tu compra.</p>
                <p>Saludos,<br>El equipo de Tortillas.</p>";

            return BaseHtml(body);
        }
    }
}
