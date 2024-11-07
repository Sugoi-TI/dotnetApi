using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnetApi.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            var claim = user.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.GivenName));
            if (claim == null)
                throw new ArgumentNullException(nameof(claim), "Claim can't be null");

            return claim.Value;
        }
    }
}