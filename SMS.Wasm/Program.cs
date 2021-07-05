using System;
using System.Text;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;

using Blazored.LocalStorage;
using Blazored.Toast;

using SMS.Wasm.Services;
namespace SMS.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // http client (used in Services (auth/student))
            builder.Services.AddScoped(
                sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)}
            );

            // 3rd party library providing access to localstorage and used to store jwt token
            builder.Services.AddBlazoredLocalStorage();
            // 3rd party library providing toast service 
            builder.Services.AddBlazoredToast();

            // configure authorization
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

            // add services
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddTransient<StudentService>();
           
            await builder.Build().RunAsync();
        }
    }
}
