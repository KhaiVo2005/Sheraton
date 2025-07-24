using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace WebTravel.Attribute
{
    public class CheckRoleAttribute : ActionFilterAttribute
    {
        private readonly string _requiredRole;

        public CheckRoleAttribute(string requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var username = session.GetString("Username");
            var role = session.GetString("Role");

            if (string.IsNullOrEmpty(username) || role != _requiredRole)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "area", "" },
                        { "controller", "Account" },
                        { "action", "Login" }
                    });
            }

            base.OnActionExecuting(context);
        }
    }
}
