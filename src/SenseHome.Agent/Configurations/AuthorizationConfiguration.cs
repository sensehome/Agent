using Microsoft.Extensions.DependencyInjection;
using SenseHome.Common.Enums;
using SenseHome.Common.Values;

namespace SenseHome.Agent.Configurations
{
    public static class AuthorizationConfiguration
    {
        public static void AddSenseHomeAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(option =>
            {
                option.AddPolicy(PolicyName.Admin, config =>
                {
                    config.RequireRole(UserType.Admin.ToIntegerString());
                });

            });
        }
    }
}
