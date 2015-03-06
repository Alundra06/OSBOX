using System.Web.Mvc;


namespace VSSystem.BaseModel.WebSites.Controllers
{
public class AuthorizeEmployees : AuthorizeAttribute
{
   protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
   {

       if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
       {
           //filterContext.Result = new RedirectResult("/Error");
          
           //filterContext.Result = new ViewResult { ViewName = "Error", ViewBag ErrorMessage = "Access denied. You do not have permission to access this resource"};
           ViewResult result = new ViewResult();
            result.ViewName = "SecurityError";
            result.ViewBag.ErrorMessage = "Access denied. You are not authorized to use this page!";
            filterContext.Result = result;
       }

   }
 }
}