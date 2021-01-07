using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SenseHome.Agent.Services;
using SenseHome.Common.Values;

namespace SenseHome.Agent.Hubs
{
    [Authorize(Policy = PolicyName.Admin)]
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
