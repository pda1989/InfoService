namespace InfoService
{
    public interface ISettingsProvider
    {
        Settings LoadSettings();
        void SaveSettings(Settings settings);
    }
}
