using Microsoft.Extensions.DependencyInjection;
using SenseHome.Agent.Services.Caching;

namespace SenseHome.Agent.Configurations
{
    public static class DependencyConfiguration
    {
        public static void AddDependentServices(this IServiceCollection services)
        {
            services.AddSingleton<ICacheService, CacheService>();
        }
    }
}
