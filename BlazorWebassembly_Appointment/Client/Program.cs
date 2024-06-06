using Blazored.Modal;
using BlazorWebassembly_Appointment.Client;
using BlazorWebassembly_Appointment.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


namespace BlazorWebassembly_Appointment.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<DoctorService>();
            builder.Services.AddScoped<AppointmentService>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<UserStateService>();
            //builder.Services.AddMudBlazorDialog();
            //builder.Services.AddScoped<NavigationManager>();

            builder.Services.AddBlazoredModal();

            await builder.Build().RunAsync();
        }
    }
}
