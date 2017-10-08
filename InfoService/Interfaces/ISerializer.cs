namespace InfoService.Interfaces
{
    public interface ISerializer
    {
        T Deserialize<T>(string message);

        string Serialize<T>(T message);
    }
}