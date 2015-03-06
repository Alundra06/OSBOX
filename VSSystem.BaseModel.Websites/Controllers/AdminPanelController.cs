using System.Web.Mvc;

namespace VSSystem.BaseModel.WebSites.Controllers
{
    public class AdminPanelController : Controller
    {
        //Allow loggedin users only
        [AuthorizeEmployees]
        //
        // GET: /AdminPanel/
         [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }
	}
}