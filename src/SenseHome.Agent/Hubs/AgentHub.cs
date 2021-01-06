using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SenseHome.Agent.Services;

namespace SenseHome.Agent.Hubs
{
    public class AgentHub : Hub<IAgentEvent>
    {
        private readonly IMqttClientService mqttClientService;

        public AgentHub(MqttClientServiceProvider mqttClientServiceProvider)
        {
            mqttClientService = mqttClientServiceProvider.MqttClientService;
        }

        public async Task PublishToMqttBroker(string topic, string payload)
        {
            await mqttClientService.PublishAsync(topic, payload);
        }
    }
}
