using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Spatem.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Spatem.Identity.Extensions
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public AppClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(JwtClaimTypes.GivenName, user.FirstName));
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(JwtClaimTypes.FamilyName, user.LastName));
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(JwtClaimTypes.Name, user.UserName));

            return principal;
        }
    }
}