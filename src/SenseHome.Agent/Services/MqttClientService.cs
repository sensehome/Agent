using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Extensions.ManagedClient;
using SenseHome.Agent.Hubs;

namespace SenseHome.Agent.Services
{
    public class MqttClientService : IMqttClientService
    {
        private readonly IManagedMqttClient mqttClient;
        private readonly IManagedMqttClientOptions options;
        private readonly IHubContext<AgentHub, IAgentEvent> hubContext;

        public MqttClientService(IManagedMqttClientOptions options, IHubContext<AgentHub, IAgentEvent> hubContext)
        {
            this.hubContext = hubContext;
            this.options = options;
            mqttClient = new MqttFactory().CreateManagedMqttClient();
            ConfigureMqttClient();
        }

        private void ConfigureMqttClient()
        {
            mqttClient.ConnectedHandler = this;
            mqttClient.DisconnectedHandler = this;
            mqttClient.ApplicationMessageReceivedHandler = this;
        }

        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            var payload = System.Text.Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload);
            await hubContext.Clients.All.Broadcast(eventArgs.ApplicationMessage.Topic, payload);
        }

        public async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            System.Console.WriteLine("Agent-Broker connected");
            await hubContext.Clients.All.AgentConnectionStatus(true);
            await mqttClient.SubscribeAsync("#");
        }

        public async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
        {
            System.Console.WriteLine($"Agent!Broker disconnected {eventArgs.ReasonCode}");
            await hubContext.Clients.All.AgentConnectionStatus(false);
        }

        public async Task PublishAsync(string topic, string payload)
        {
            var applicationMessage = new MqttApplicationMessageBuilder().WithTopic(topic)
                                                                        .WithPayload(payload)
                                                                        .Build();
            var manegedApplicationMessage = new ManagedMqttApplicationMessageBuilder().WithApplicationMessage(applicationMessage)
                                                                                      .Build();
            await mqttClient.PublishAsync(manegedApplicationMessage);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await mqttClient.StartAsync(options);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await mqttClient.StopAsync();
        }

        public bool IsMqttClientConnected()
        {
            if (mqttClient.IsStarted)
            {
                return mqttClient.IsConnected;
            }
            return false;
        }
    }
}
