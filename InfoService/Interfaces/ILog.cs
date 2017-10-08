namespace InfoService.Interfaces
{
    public enum LogType : byte
    {
        Information,
        Error
    }

    public interface ILog
    {
        void Write(string message, LogType type = LogType.Information);
    }
}