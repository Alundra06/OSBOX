using System.Web;
using System.Web.Mvc;
using System.Linq;


namespace OSBOX.Common.Infrastructure
{
    public class AuthorizeEmployeesOnly : AuthorizeAttribute
{
 
       protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
       {

           if (!this.Roles.Split(',').Any(filterContext.HttpContext.User.IsInRole))
           {
               ViewResult result = new ViewResult();
               result.ViewName = "SecurityError";
               result.ViewBag.ErrorMessage = "Access denied. You are not allowed to use this page!";
               filterContext.Result = result;
           }

         


       }
      

 }
}
