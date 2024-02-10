using System;
using Microsoft.AspNetCore.Http;
namespace SW.HomeVisits.Auth.Helper
{
    public class CultureService:ICultureService
    {
        public string GetCulture()
        {
            //var userLangs = Headers["Accept-Language"].ToString();
            //var firstLang = userLangs.Split(',').FirstOrDefault();
            //var defaultLang = string.IsNullOrEmpty(firstLang) ? "en" : firstLang;
            //return defaultLang;
            return "";
        }
    }
}
