using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using BlazorAppC2Corridor.Data;
using System.Security.Claims;

namespace BlazorAppC2Corridor.Services
{
    public class CustomClaimsPrincipalFactory :
    UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public CustomClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor) { }
        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            if (!string.IsNullOrEmpty(user.Firstname))
            {
                if (principal.Identity != null)
                {
                    ((ClaimsIdentity)principal.Identity).AddClaims(
                    new[] { new Claim(ClaimTypes.GivenName, user.Firstname) });
                }
            }
            if (!string.IsNullOrEmpty(user.Lastname))
            {
                if (principal.Identity != null)
                {
                    ((ClaimsIdentity)principal.Identity).AddClaims(
                    new[] { new Claim(ClaimTypes.Surname, user.Lastname) });
                }
            }
            return principal;
        }
    }
}
