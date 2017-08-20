namespace InfoService
{
    public interface IMessageHandler
    {
        string ProcessMessage(InputMessage message);
        string ProcessMessage(string message);
    }
}
