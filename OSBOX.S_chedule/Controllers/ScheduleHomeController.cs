using OSBOX.Common.Infrastructure;
using System.Web.Mvc;

namespace OSBOX.S_chedule.Controllers
{
    //Allow loggedin users only
    [Authorize]
    public class ScheduleHomeController : Controller
    {
        //
        // GET: /ScheduleHome/
        //Allow loggedin users only
        [AuthorizeAuthenticatedOnly]
        //Allow Users with the roles: Administrator or Employee only
        [AuthorizeEmployeesOnly(Roles = "Administrator,Employee")]
        public ActionResult Index()
        {
            return View();
        }
    }
}