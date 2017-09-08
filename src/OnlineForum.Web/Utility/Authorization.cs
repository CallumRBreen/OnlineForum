using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineForum.Web.Utility
{
    public static class Authorization
    {
        public static int GetCurrentUserId(this HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return userId == null ? 0 : Convert.ToInt32(userId);
        }

        public static int GetCurrentUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return userId == null ? 0 : Convert.ToInt32(userId);
        }
    }
}
