namespace ChatApp.Server.Services
{
    public interface IRabbitMqService : IDisposable
    {
        void SendMessage(string message, string routingKey);
        void RegisterConsumer(string queue, Action<string> messageReceivedCallback);
    }
}
