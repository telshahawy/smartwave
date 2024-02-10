using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Configuration
{
    public static class DataBaseConnectionString
    {
        public static string GetDataConnectionStringFromConfig()
        {
            return new DatabaseConfiguration().GetDataConnectionString();
        }
    }

    public class DatabaseConfiguration : ConfigurationBase
    {
        private static string AuthConnectionKey = "ConnectionString";
        private static string HomeVisitConnectionKey = "ConnectionString";

        public string GetDataConnectionString()
        {
            return GetConfiguration().GetSection("AppSettings").GetValue<string>(HomeVisitConnectionKey);//.GetConnectionString(HomeVisitConnectionKey);
        }

        public string GetAuthConnectionString()
        {
            return GetConfiguration().GetConnectionString(AuthConnectionKey);
        }
    }
}
