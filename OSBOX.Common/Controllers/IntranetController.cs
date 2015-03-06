using System.Web.Mvc;


namespace OSBOX.Common.Controllers
{
    public class IntranetController : Controller
    {
        //
        // GET: /Intranet/
        public ActionResult Index()
        {
            return RedirectToAction("Home", "Index");
           // return View("EmployeeLogin");
        }
	}
}