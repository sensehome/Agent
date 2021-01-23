using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SenseHome.Agent.Services;
using SenseHome.Agent.Services.Caching;
using SenseHome.Common.Values;

namespace SenseHome.Agent.Hubs
{
    [Authorize(Policy = PolicyName.Admin)]
    public class AgentHub : Hub<IAgentEvent>
    {
        private readonly IMqttClientService mqttClientService;
        private readonly ICacheService cacheService;

        public AgentHub(MqttClientServiceProvider mqttClientServiceProvider, ICacheService cacheService)
        {
            mqttClientService = mqttClientServiceProvider.MqttClientService;
            this.cacheService = cacheService;
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.AgentConnectionStatus(mqttClientService.IsMqttClientConnected());
            foreach(var topic in cacheService.GetKeys())
            {
                Clients.Caller.Broadcast(topic, cacheService.GetValueOrDefault(topic));
            }
            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception exception)
        {
            foreach (var topic in cacheService.GetKeys())
            {
                Console.WriteLine(topic, cacheService.GetValueOrDefault(topic));
            }
            return base.OnDisconnectedAsync(exception);
        }


        public async Task PublishToMqttBroker(string topic, string payload)
        {
            await mqttClientService.PublishAsync(topic, payload);
        }
    }
}
