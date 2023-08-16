using ChatApp.Server.Models;

namespace ChatApp.Server.Services
{
    public class CommandDetectionService : ICommandDetectionService
    {
        public CommandInfo DetectCommand(string message)
        {
            if (message.StartsWith("/stock="))
            {
                string stockCode = message[7..]; // Remove "/stock="
                return new CommandInfo { IsCommand = true, StockCode = stockCode };
            }
            else
            {
                return new CommandInfo { IsCommand = false, StockCode = string.Empty };
            }
        }
    }
}
