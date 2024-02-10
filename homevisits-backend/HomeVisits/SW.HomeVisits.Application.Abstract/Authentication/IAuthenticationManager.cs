using System;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
namespace SW.HomeVisits.Application.Abstract.Authentication
{
    public interface IAuthenticationManager
    {
        //Task CreateChemistUser(User user,string password);
        Task<List<Claim>> GetUserAsync(ClaimsPrincipal sub);
    }
}