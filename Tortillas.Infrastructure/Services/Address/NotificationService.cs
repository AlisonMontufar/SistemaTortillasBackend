using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tortillas.Domain.Interfaces.Repositories;

namespace Tortillas.Infrastructure.Services.Address
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5149/api/v1/Notification/";

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendRegistrationLinkAsync(string email, int roleId = 2)
        {
            var payload = new { email, roleId };
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}send-registration-link", content);
            return response.IsSuccessStatusCode;
        }
    }
}
