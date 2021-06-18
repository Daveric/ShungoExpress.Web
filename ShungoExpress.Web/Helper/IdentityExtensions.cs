using System.Security.Claims;
using System.Security.Principal;

namespace ShungoExpress.Web.Helper
{
  public static class IdentityExtensions
  {
    public static string GetClaimValue(this IIdentity identity, string key)
    {
      var claim = ((ClaimsIdentity)identity).FindFirst(key);
      return claim != null ? claim.Value : string.Empty;
    }

    public static void AddUpdateClaim(this IIdentity identity, string key, string value)
    {
      var claimsId = (ClaimsIdentity)identity;
      // check for existing claim and remove it
      var existingClaim = claimsId.FindFirst(key);
      if (existingClaim != null)
        claimsId.RemoveClaim(existingClaim);

      // add new claim
      claimsId.AddClaim(new Claim(key, value));
    }
  }
}
