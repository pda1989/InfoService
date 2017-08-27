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

        public void LoadSettings(Settings settings)
        {
            string text = File.ReadAllText(_fileName);
            var currentSettings = _serializer?.Deserialize<Settings>(text);
            if (currentSettings != null)
            {
                settings.TimerInterval = currentSettings.TimerInterval;
                settings.ServerName = currentSettings.ServerName;
                settings.ServerPort = currentSettings.ServerPort;
            }

        }

        public void SaveSettings(Settings settings)
        {
            string text = _serializer?.Serialize(settings);
            File.WriteAllText(_fileName, text);
        }
    }
}
