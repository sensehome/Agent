using Microsoft.AspNetCore.Builder;
using SenseHome.Agent.Hubs;

namespace SenseHome.Agent.Configurations
{
    public static class SignalRConfiguration
    {
        public static void UseConfiguredSignalR(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AgentHub>("/agenthub");
            });
        }
    }
}
