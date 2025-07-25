using Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PushService : IPushService
    {
        private readonly HttpClient _client;

        public PushService(HttpClient client)
        {
            _client = client;
        }

        public async Task SendNotificationAsync(string expoPushToken, string title, string body, string data = "")
        {
            var payload = new
            {
                to = expoPushToken,
                sound = "default",
                title,
                body,
                data
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://exp.host/--/api/v2/push/send")
            {
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
