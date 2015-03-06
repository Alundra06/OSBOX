using System.Web.Mvc;

namespace VSSystem.BaseModel.WebSites.Controllers
{
    //Allow loggedin users only
    [AuthorizeEmployees]
    public class ScheduleHomeController : Controller
    {
        //
        // GET: /ScheduleHome/
         [Authorize(Roles = "Administrator, Employee,FrontDesk,SuperAdmin")]
        public ActionResult Index()
        {
            return View();
        }
	}
}