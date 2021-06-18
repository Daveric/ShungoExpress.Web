using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ShungoExpress.Web.Data.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShungoExpress.Web.Helper
{
  public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
  {
    public AppClaimsPrincipalFactory(UserManager<User> userManager
      , RoleManager<IdentityRole> roleManager
      , IOptions<IdentityOptions> optionsAccessor)
      : base(userManager, roleManager, optionsAccessor)
    { }
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
      var identity = await base.GenerateClaimsAsync(user);
      identity.AddClaim(new Claim("FirstName", user.FirstName ?? ""));
      return identity;
    }
  }
}
