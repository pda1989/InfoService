namespace InfoService
{
    public interface IServiceCommand
    {
        OutputMessage Execute(InputMessage message);
    }
}
