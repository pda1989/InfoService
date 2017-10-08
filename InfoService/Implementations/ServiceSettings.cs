using InfoService.Interfaces;

namespace InfoService.Implementations
{
    public class ServiceSettings
    {
        protected ISettingsProvider _provider;

        public string ServerName { get; set; } = string.Empty;
        public int ServerPort { get; set; } = 0;
        public int TimerInterval { get; set; } = 60000;

        public ServiceSettings(ISettingsProvider provider)
        {
            _provider = provider;
        }

        public void LoadSettings() =>
            _provider?.LoadSettings(this);

        public void SaveSettings() =>
            _provider?.SaveSettings(this);
    }
}