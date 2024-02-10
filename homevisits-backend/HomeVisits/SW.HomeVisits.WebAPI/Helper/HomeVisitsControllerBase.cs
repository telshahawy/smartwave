using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Globalization;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.WebAPI.Models;
using Microsoft.AspNet.Identity;
using SW.HomeVisits.WebAPI.CustomAttribute;

namespace SW.HomeVisits.WebAPI.Helper
{
    [AuthorizeByUserPermission]
    public class HomeVisitsControllerBase : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public CultureInfo GetCulture()
        {
            var userLangs = Request.Headers["Accept-Language"].SingleOrDefault().ToString();
            var firstLang = userLangs.Split(',').FirstOrDefault();
            var defaultLang = string.IsNullOrEmpty(firstLang) ? "en" : firstLang;
            return new CultureInfo(defaultLang);
            //options.DefaultRequestCulture = new RequestCulture(defaultLang);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public CultureNames GetCultureName()
        {
            var userLangs = Request?.Headers["Accept-Language"].SingleOrDefault().ToString();
            var firstLang = userLangs?.Split(',').FirstOrDefault();
            var defaultLang = string.IsNullOrEmpty(firstLang) ? "en" : firstLang;

            return defaultLang == "ar" ? CultureNames.ar : CultureNames.en;
            //options.DefaultRequestCulture = new RequestCulture(defaultLang);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public LoggedInUserInfo GetCurrentUserId()
        {
            if(User.Identity.IsAuthenticated == false)
            {
                throw new UnauthorizedAccessException();
            }
            return new LoggedInUserInfo
            {
                UserId = Guid.Parse(User.Identity.GetUserId()),
                ClientId = Guid.Parse(User.Claims.SingleOrDefault(x => x.Type == "Client")?.Value)

            };
        }
    
    }
}
