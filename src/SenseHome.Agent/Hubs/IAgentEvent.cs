using System.Threading.Tasks;

namespace SenseHome.Agent.Hubs
{
    public interface IAgentEvent
    {
        Task Broadcast(string topic, string payload);
        Task AgentConnectionStatus(bool isConnected);
    }
}
