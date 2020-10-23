using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SenseHome.Agent.Settings;

namespace SenseHome.Agent.Configurations
{
    public static class CorsConfiguration
    {
        public static void AddConfiuredCors(this IServiceCollection services, IConfiguration configuration)
        {
            var corsSettings = new CorsSettings();
            configuration.GetSection(nameof(CorsSettings)).Bind(corsSettings);
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .WithOrigins(corsSettings.AllowedHosts)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
        }

        public static void UseConfiguredCors(this IApplicationBuilder app)
        {
            app.UseCors();
        }
    }
}
