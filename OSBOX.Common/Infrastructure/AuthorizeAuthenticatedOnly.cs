using System.Web.Mvc;
using System.Linq;


namespace OSBOX.Common.Infrastructure
{
public class AuthorizeAuthenticatedOnly : AuthorizeAttribute
{
   protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
   {

       if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
       {
           ViewResult result = new ViewResult();
            result.ViewName = "SecurityError";
            result.ViewBag.ErrorMessage = "Access denied. You are not authorized to use this page! Please try to log in";
            filterContext.Result = result;
       }
       

   }
 }
}