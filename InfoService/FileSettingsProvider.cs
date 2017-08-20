using System.IO;

namespace InfoService
{
    public class FileSettingsProvider : ISettingsProvider
    {
        protected string _fileName;
        protected ISerializer _serializer;


        public FileSettingsProvider(string fileName, ISerializer serializer)
        {
            _fileName = fileName;
            _serializer = serializer;
        }

        public Settings LoadSettings()
        {
            string text = File.ReadAllText(_fileName);
            var settings = _serializer?.Deserialize<Settings>(text);
            return settings;
        }

        public void SaveSettings(Settings settings)
        {
            string text = _serializer?.Serialize(settings);
            File.WriteAllText(_fileName, text);
        }
    }
}
