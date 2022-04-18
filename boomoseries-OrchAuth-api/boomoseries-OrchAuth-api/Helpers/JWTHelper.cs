using Microsoft.AspNetCore.Http;

namespace boomoseries_OrchAuth_api.Helpers
{
    public static class JWTHelper
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if(httpContext.User is null)
            {
                return string.Empty;
            }
            return httpContext.User.Identity.Name; //returns user id from the token
        }
    }
}
