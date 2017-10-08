using InfoService.Implementations;

namespace InfoService.Interfaces
{
    public interface ISettingsProvider
    {
        void LoadSettings(ServiceSettings settings);

        void SaveSettings(ServiceSettings settings);
    }
}