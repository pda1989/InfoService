namespace InfoService
{
    public interface ISettingsProvider
    {
        void LoadSettings(Settings settings);
        void SaveSettings(Settings settings);
    }
}
