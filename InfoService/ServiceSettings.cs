namespace InfoService
{
    public class ServiceSettings : Settings
    {
        private static ServiceSettings _instance;
        public static ServiceSettings GetInstance()
        {
            if (_instance == null)
                _instance = new ServiceSettings();

            return _instance;
        }


        private ISettingsProvider _provider;


        private ServiceSettings() {}

        public void SetSettingsProvider(ISettingsProvider provider)
        {
            _provider = provider;
        }

        public void LoadSettings()
        {
            _provider?.LoadSettings(this);
        }

        public void SaveSettings()
        {
            _provider?.SaveSettings(this);
        }
    }
}
