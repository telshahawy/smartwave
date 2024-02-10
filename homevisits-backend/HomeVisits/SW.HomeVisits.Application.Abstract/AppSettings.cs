using System;
namespace SW.HomeVisits.Application.Abstract
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string LogPath { get; set; }
        public string FireBaseServerKey { get; set; }
        public string FireBaseSenderId { get; set; }
        public string DeviceToken { get; set; }
        public string GoogleMapAPIKey { get; set; }
        public string GoogleMapClientID { get; set; }
        public string AssignAndOptimizeVisitsHangfireCronExpression { get; set; }
        public String JwtBearerAuthority { get; set; }
    }
}
