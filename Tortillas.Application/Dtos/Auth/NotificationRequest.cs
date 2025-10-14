using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Tortillas.Application.Notifications;

namespace Tortillas.Application.Dtos.Auth
{
    public class NotificationRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public NotificationType Type { get; set; }
    }
}