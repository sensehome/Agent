using System;
using MQTTnet.Extensions.ManagedClient;

namespace SenseHome.Agent.Options
{
    public class AspCoreManagedMqttClientOptionBuilder : ManagedMqttClientOptionsBuilder
    {
        public IServiceProvider ServiceProvider { get; }

        public AspCoreManagedMqttClientOptionBuilder(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
