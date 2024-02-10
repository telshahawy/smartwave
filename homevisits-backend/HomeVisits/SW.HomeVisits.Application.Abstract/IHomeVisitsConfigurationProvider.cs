using System;
namespace SW.HomeVisits.Application.Abstract
{
    public interface IHomeVisitsConfigurationProvider
    {
        string ConnectionString { get; }
        string LogPath { get; }
        string FireBaseServerKey { get; }
        string FireBaseSenderId { get; }
        string DeviceToken { get; }
    }
}
