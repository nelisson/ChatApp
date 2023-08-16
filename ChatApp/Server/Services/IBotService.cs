namespace ChatApp.Server.Services
{
    public interface IBotService
    {
        Task<string> ProcessStockCommand(string stockCode);
        void SendMessageToBroker(string stockMessage, int chatroomId);
    }
}
