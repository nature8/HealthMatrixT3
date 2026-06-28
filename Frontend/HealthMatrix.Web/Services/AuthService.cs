using System.Net.Http;  
using System.Net.Http.Json;
using System.Text.Json;
using HealthMatrix.Web.ViewModels;  

namespace HealthMatrix.Web.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            var request =new{
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role
            };
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<LoginResponse?> LoginAsync(LoginViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", model);
            if (!response.IsSuccessStatusCode)
            {
                return null; // Return null to indicate failure
            }
            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }

        public class LoginResponse
        {
            public string Token { get; set; } = string.Empty;
            public UserDto User { get; set; } = new();
        }

        public class UserDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;
        }
    }
}