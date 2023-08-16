using ChatApp.Server.Models;

namespace ChatApp.Server.Services
{
    public interface ICommandDetectionService
    {
        CommandInfo DetectCommand(string message);
    }
}
