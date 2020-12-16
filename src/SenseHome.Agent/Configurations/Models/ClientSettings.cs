namespace SenseHome.Agent.Configurations.Models
{
    public class ClientSettings
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int AutoReconnectInSec { get; set; }
    }
}
