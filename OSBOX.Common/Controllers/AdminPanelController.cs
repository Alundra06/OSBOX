
using OSBOX.Common.Infrastructure;
using System.Web.Mvc;

namespace OSBOX.Common.Controllers
{
    public class AdminPanelController : Controller
    {
        //Allow loggedin users only
        [AuthorizeAuthenticatedOnly]
        //
        // GET: /AdminPanel/
         [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }
	}
}