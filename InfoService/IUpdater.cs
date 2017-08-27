using System.Reflection;

namespace InfoService
{
    public interface IUpdater
    {
        void Update(Assembly assembly, string updateFilePath, string fileHash, string tempFileName);
    }
}
