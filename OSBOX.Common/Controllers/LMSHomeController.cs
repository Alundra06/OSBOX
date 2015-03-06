using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scheduleMVC.Controllers
{
    public class LMSHomeController : Controller
    {
        // GET: LMSHome
        public ActionResult Index()
        {
            var result = new FilePathResult("~/Content/EmployeeManual/index.html", "text/html");
            //return result;
            return Redirect("~/Content/EmployeeManual/index.html");
           
        }
    }
}