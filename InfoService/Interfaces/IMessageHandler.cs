using InfoService.Models;

namespace InfoService.Interfaces
{
    public interface IMessageHandler
    {
        string ProcessMessage(InputMessage message);

        string ProcessMessage(string message);
    }
}