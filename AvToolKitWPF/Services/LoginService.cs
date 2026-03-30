using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AvToolKitWPF.Services.Interfaces;

namespace AvToolKitWPF.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _client;
        public LoginService(HttpClient client) 
        { 
            _client = client;
        }
        public async Task<string> Auth(string username, string password)
        {
            var loginData = new { username, password };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/Login", content);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                return token;
            }
            else
            {
                throw new Exception("Login failed");
            }
        }
    }
}
