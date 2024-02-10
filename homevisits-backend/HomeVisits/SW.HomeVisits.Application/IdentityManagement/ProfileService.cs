using System;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using SW.HomeVisits.Application.Abstract.Authentication;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
namespace SW.HomeVisits.Application.IdentityManagement
{
    public class ProfileService : IProfileService
    {
        private readonly IAuthenticationManager _authenticationManager;
        public ProfileService(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //>Processing
            var claims = await _authenticationManager.GetUserAsync(context.Subject);
            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            //>Processing
            //var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = true;// (user != null) && user.IsActive;
        }
    }
}
