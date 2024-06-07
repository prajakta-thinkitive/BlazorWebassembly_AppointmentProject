using Azure;
using BlazorWebassembly_Appointment.Shared;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace BlazorWebassembly_Appointment.Client.Services
{
    public class AppointmentService
    {
        private readonly HttpClient _httpClient;

        public AppointmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAppointmentAsync(AppointmentModel appointment)
        {
            try
            {
               var a=  await _httpClient.PostAsJsonAsync("api/appointment/Create", appointment);
                a.EnsureSuccessStatusCode();
            }
            catch(Exception ex) 
            { 
               
            }   
            
        }

        public async Task<List<AppointmentModel>> GetAppointmentsByDoctor(int doctorId)
        {
            return await _httpClient.GetFromJsonAsync<List<AppointmentModel>>($"api/doctors/{doctorId}/appointments");
        }


        public async Task<List<AppointmentModel>> GetAllAppointments()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<AppointmentModel>>("api/appointment/GetAll");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching appointments: {ex.Message}");
                return null;
            }
        }

        public async Task<List<string>> GetBookedSlots(int doctorId, DateTime date)
        {
            var response = await _httpClient.GetAsync($"api/appointment/{doctorId}/{date.ToString("yyyy-MM-dd")}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<string>>();
        }
    }
}
