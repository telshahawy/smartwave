using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SW.HomeVisits.Application.Abstract;

namespace SW.HomeVisits.Auth
{

    public class HomeVisitsConfigurationProvider : IHomeVisitsConfigurationProvider
    {
        private readonly AppSettings _appSettings;
        public HomeVisitsConfigurationProvider(IOptions<AppSettings> settings)
        {
            _appSettings = settings.Value;
        }
        public string ConnectionString => _appSettings.ConnectionString;
        public string LogPath => _appSettings.LogPath;

        public string FireBaseServerKey => _appSettings.FireBaseServerKey;

        public string FireBaseSenderId => _appSettings.FireBaseSenderId;

        public string DeviceToken => _appSettings.DeviceToken;
    }
}