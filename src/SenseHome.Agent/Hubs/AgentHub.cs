using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SenseHome.Agent.Hubs
{
    public class AgentHub : Hub<IAgentEvent>
    {
        public AgentHub()
        {
            
        }

        public Task RequestMqttBroker(string topic, string payload)
        {
            throw new NotImplementedException();
        }
    }
}
