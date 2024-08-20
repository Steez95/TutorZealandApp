using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

public class AdminRoleAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var httpContext = context.HttpContext;
        var userRole = httpContext.Session.GetString("role");

        if (userRole != "admin")
        {
            context.Result = new RedirectToPageResult("/AccessDenied");
        }

        base.OnActionExecuting(context);
    }
}
