using Azure;
using BlazorWebassembly_Appointment.Shared;
using BlazorWebassembly_Appointment.Shared.Dtos;
using System.Net.Http.Json;

namespace BlazorWebassembly_Appointment.Client.Services
{
    public class AuthService
    {   
        private readonly HttpClient _httpClient;

        private string _loggedInUserName;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Register(RegisterUser model, string selectedRole)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Authentication/Register?role={selectedRole}", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Login(LoginUser model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Authentication/Login", model);
            return response.IsSuccessStatusCode;
        }

        public async Task Logout()
        {
            await _httpClient.PostAsync("api/Authentication/Logout",null);
        }

        public async Task<CurrentUserDto> GetLoggedInUser()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<CurrentUserDto>("api/Authentication/GetCurrentUser");
                return response;
            }
            catch (Exception ex)
            {
                return new CurrentUserDto();
            }
        }

    }
}

