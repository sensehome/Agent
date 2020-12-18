using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using SenseHome.Agent.Configurations.Models;
using SenseHome.Agent.Options;
using SenseHome.Agent.Services;

namespace SenseHome.Agent.Configurations
{
    public static class MqttClientConfiguration
    {
        public static void AddHostedMqttClient(this IServiceCollection services, IConfiguration configuration)
        {
            var agentMqttSettings = new AgentMqttSettings();
            configuration.GetSection(nameof(AgentMqttSettings)).Bind(agentMqttSettings);
            services.AddConfiguredMqttClientService(optionBuilder =>
            {
                optionBuilder
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithClientId(agentMqttSettings.ClientSettings.Id)
                    .WithCredentials(agentMqttSettings.ClientSettings.UserName, agentMqttSettings.ClientSettings.Password)
                    .WithTcpServer(agentMqttSettings.BrokerSettings.Host, agentMqttSettings.BrokerSettings.Port)
                    .Build());
            });
        }

        private static IServiceCollection AddConfiguredMqttClientService(this IServiceCollection services,
                    Action<AspCoreManagedMqttClientOptionBuilder> configuration)
        {
            services.AddSingleton<IManagedMqttClientOptions>(serviceProvider =>
            {
                var optionsBuilder = new AspCoreManagedMqttClientOptionBuilder(serviceProvider);
                configuration(optionsBuilder);
                return optionsBuilder.Build();
            });
            services.AddSingleton<MqttClientService>();
            services.AddSingleton<IHostedService>(serviceProvider =>
            {
                return serviceProvider.GetService<MqttClientService>();
            });
            services.AddTransient<MqttClientServiceProvider>(serviceProvider =>
            {
                var mqttClientService = serviceProvider.GetService<MqttClientService>();
                var mqttClientServiceProvider = new MqttClientServiceProvider(mqttClientService);
                return mqttClientServiceProvider;
            });
            return services;
        }
    }
}
