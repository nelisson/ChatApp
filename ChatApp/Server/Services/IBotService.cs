namespace ChatApp.Server.Services
{
    public interface IBotService
    {
        Task<string> ProcessStockCommand(string stockCode);
    }
}
