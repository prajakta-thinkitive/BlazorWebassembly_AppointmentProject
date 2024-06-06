//using BlazorWebassembly_Appointment.Shared;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Numerics;

//namespace BlazorWebassembly_Appointment.Client.Services
//{
//    public class RegisterDoctorService
//    {
//        private readonly HttpClient _httpClient;

//        public RegisterDoctorService(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        public async Task<List<RegisterDoctors>> GetAllDoctors()
//        {
//            return await _httpClient.GetFromJsonAsync<List<RegisterDoctors>>("api/RegisterDoctors/GetAllDoctors");
//        }
//    }
//}
