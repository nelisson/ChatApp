namespace ChatApp.Server.Services
{
    public interface IRabbitMqService : IDisposable
    {
        void SendMessage(string message, string routingKey);
    }
}
