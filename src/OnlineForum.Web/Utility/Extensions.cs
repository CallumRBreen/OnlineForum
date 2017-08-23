using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineForum.Web.Utility
{
    public static class Extensions
    {
        public static int GetCurrentUserId(this HttpContext httpContext)
        {
            return Convert.ToInt32(httpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
