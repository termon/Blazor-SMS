using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using SMS.Data.Services;
using SMS.Rest.Helpers;
using SMS.Rest.Filters;

namespace SMS.Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {           
            // configure jwt authentication using extension method                      
            services.AddJwtAuthentication(Configuration);
            
            // enable cors processing
            services.AddCors(); 
            
            // to turn off standard api validation filter and use custom ValidationFilter
            // services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            // services.AddControllers( opts => opts.Filters.Add(new ValidationFilter())
            
            // configure controller, registering fluent validation validator classes
            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            // configure the student service in DI
            services.AddSingleton<IStudentService,StudentService>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging(); // blazor

                StudentServiceSeeder.Seed(new StudentService());
            }

            app.UseHttpsRedirection();
            
            app.UseBlazorFrameworkFiles(); // blazor
            app.UseStaticFiles(); // blazor

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication(); // Must be after UseRouting()
            app.UseAuthorization(); // Must be after UseAuthentication()     
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
                endpoints.MapFallbackToFile("index.html");   // blazor       
            });
        }
    }
}
