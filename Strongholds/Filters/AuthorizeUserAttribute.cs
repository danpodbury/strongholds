using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Strongholds.Filters
{
    public class AuthorizeUserAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Session.GetString("token");
            if (token == null)
                context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }

}
