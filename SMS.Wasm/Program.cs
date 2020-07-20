using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using SMS.Wasm.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using FluentValidation;
using Blazored.Toast;


//https://devblogs.microsoft.com/aspnet/blazor-webassembly-3-2-0-preview-2-release-now-available/
namespace SMS.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            // http client (used in Services (auth/student))
            builder.Services.AddSingleton(
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

            // configure fluid validation
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            // add auth service
            builder.Services.AddScoped<AuthService>();
     
            // add student service
            builder.Services.AddScoped<StudentService>();
           
            await builder.Build().RunAsync();
        }
    }
}
