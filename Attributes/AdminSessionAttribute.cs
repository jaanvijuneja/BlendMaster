using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Tools;

namespace WebApplication2.Attributes
{
    public class AdminSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session.GetObject<string>("AdminSession");
            if (string.IsNullOrEmpty(session))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
    }
}
