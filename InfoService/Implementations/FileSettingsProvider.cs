using InfoService.Interfaces;
using System.IO;
using System.Reflection;

namespace InfoService.Implementations
{
    public class FileSettingsProvider : ISettingsProvider
    {
        protected string _fileName;
        protected ISerializer _serializer;

        public string FileName
        {
            get => _fileName;
            set => _fileName = value;
        }

        public FileSettingsProvider(ISerializer serializer)
        {
            string dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string settingsFileName = Path.Combine(dirName, "ServiceSettings.json");

            _fileName = settingsFileName;
            _serializer = serializer;
        }

        public void LoadSettings(ServiceSettings settings)
        {
            string text = File.ReadAllText(_fileName);
            var currentSettings = _serializer?.Deserialize<ServiceSettings>(text);
            if (currentSettings != null)
            {
                settings.TimerInterval = currentSettings.TimerInterval;
                settings.ServerName = currentSettings.ServerName;
                settings.ServerPort = currentSettings.ServerPort;
            }
        }

        public void SaveSettings(ServiceSettings settings)
        {
            string text = _serializer?.Serialize(settings);
            File.WriteAllText(_fileName, text);
        }
    }
}