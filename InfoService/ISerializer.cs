namespace InfoService
{
    public interface ISerializer
    {
        string Serialize<T>(T message);
        T Deserialize<T>(string message);
    }
}
