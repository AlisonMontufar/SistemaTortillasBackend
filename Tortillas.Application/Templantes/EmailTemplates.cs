using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tortillas.Application.Templantes
{
    namespace Tortillas.Application.Templates
    {
        public static class EmailTemplates
        {
            public static string GetWelcomeBody(string nombre) =>
                $"Hola {nombre},<br><br>" +
                $"¡Bienvenido a Tortillas! Tu cuenta ha sido registrada exitosamente.<br>" +
                $"Esperamos que disfrutes de nuestros servicios.<br><br>" +
                $"Saludos,<br>El equipo de Tortillas.";

            public static string GetRecoveryBody(string nombre, string code) =>
                $"Hola {nombre},<br><br>" +
                $"Tu código de recuperación es: <b>{code}</b><br>" +
                $"El código expirará en 15 minutos.<br><br>" +
                $"Si no solicitaste este cambio, ignora este correo.";

            public static string GetPasswordResetConfirmation(string nombre) =>
                $"Hola {nombre},<br><br>" +
                $"Tu contraseña ha sido restablecida exitosamente.<br>" +
                $"Si no realizaste esta acción, por favor contacta al soporte de inmediato.<br><br>" +
                $"Saludos,<br>El equipo de Tortillas.";

            public static string GetAccountBlockedBody(string nombre) =>
                $"Hola {nombre},<br><br>" +
                $"Tu cuenta ha sido bloqueada por motivos de seguridad.<br>" +
                $"Por favor contacta al soporte para más información.<br><br>" +
                $"Saludos,<br>El equipo de Tortillas.";

            public static string GetPedidoConfirmadoBody(string nombre) =>
                $"Hola {nombre},<br><br>" +
                $"Tu pedido ha sido confirmado y está en proceso.<br>" +
                $"Gracias por tu compra.<br><br>" +
                $"Saludos,<br>El equipo de Tortillas.";
        }
    }

}


