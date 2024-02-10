using System;
using SW.HomeVisits.Application.Abstract.Authentication;
using SW.HomeVisits.Application.IdentityManagement.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastructure.AspIdentity.Authentication
{
    public class AuthenticationManager:IAuthenticationManager
    {
        private readonly UserManager<User> _userManager;
        public AuthenticationManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateAsync(User user,string password)
        {
         
        }
        public async Task<List<Claim>> GetUserAsync(ClaimsPrincipal sub)
        {
            var user = await _userManager.GetUserAsync(sub);
            return new List<Claim>
            {
                new Claim("Email", string.IsNullOrWhiteSpace(user.Email)? "":user.Email ),
                new Claim("UserName", user.UserName),
                new Claim("UserType", user.UserType.ToString()),
                new Claim("HomeVisitsClientId", ""),
                new Claim("FullName", ""),
                new Claim("Client", user.ClientId.ToString()),
            };
        }
    }
}
