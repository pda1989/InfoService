using System.Reflection;

namespace InfoService.Interfaces
{
    public interface IUpdater
    {
        void Update(Assembly assembly, string updateFilePath, string fileHash);
    }
}