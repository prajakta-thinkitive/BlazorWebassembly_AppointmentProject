using BlazorWebassembly_Appointment.Shared;
using BlazorWebassembly_Appointment.Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorWebassembly_Appointment.Client.Services
{
    public class DoctorService
    {
        private readonly HttpClient _httpClient;

        public DoctorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<DoctorDetail>> GetDoctors()
        {
            return await _httpClient.GetFromJsonAsync<List<DoctorDetail>>("api/doctors");
        }

        public async Task<DoctorDetail> GetDoctor(int id)
        {
            return await _httpClient.GetFromJsonAsync<DoctorDetail>($"api/doctors/{id}");
        }

        public async Task CreateDoctor(DoctorDetail doctor)
        {
            await _httpClient.PostAsJsonAsync("api/doctors", doctor);
        }

        public async Task UpdateDoctor(DoctorDetail doctor)
        {
            await _httpClient.PutAsJsonAsync($"api/doctors/{doctor.Id}", doctor);
        }

        public async Task DeleteDoctor(int id)
        {
            await _httpClient.DeleteAsync($"api/doctors/{id}");
        }

        public async Task<List<AppointmentModel>> GetAppointmentsByDoctor(int doctorId)
        {
            return await _httpClient.GetFromJsonAsync<List<AppointmentModel>>($"api/doctors/{doctorId}/appointments");
        }


        //SerchhByDoctorNAme
        public async Task<List<DoctorDetail>> SearchDoctors(string name)
        {
            var response = await _httpClient.GetAsync($"api/doctors/search?name={name}");
            response.EnsureSuccessStatusCode();  // This line ensures the HTTP response is successful.
            return await response.Content.ReadFromJsonAsync<List<DoctorDetail>>();
        }

        //Fetch AllDoctorList

        public async Task<List<DoctorDetail>> GetDoctorsList()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<DoctorDetail>>("api/doctors/GetDoctorsList");
            }
            catch (Exception ex)
            {
                // Handle exception 
                return new List<DoctorDetail>();
            }
        }

        public async Task UpdateDoctorStatus(int doctorId, bool isChecked)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/doctors/{doctorId}/status", isChecked);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<DoctorDetail>> GetActiveDoctors()
        {
            var response = await _httpClient.GetAsync("api/doctors/GetActiveDoctors");
            response.EnsureSuccessStatusCode();

            var activeDoctors = await response.Content.ReadFromJsonAsync<List<DoctorDetail>>();
            return activeDoctors;
        }

        public async Task<DoctorDetail> GetDoctorById(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<DoctorDetail>($"api/doctors/{id}");
            return response;
        }

        //New 
        public async Task<DoctorDetail> GetCurrentDoctor()
        {
            return await _httpClient.GetFromJsonAsync<DoctorDetail>("api/doctors/me");
        }

    }
}