using OSBOX.Common.Controllers;
using OSBOX.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSBOX.WebSites.Demo.Controllers
{
    public class HomeController : Controller
    {

        ICurrentUser Currentuser;
        IFileContext FileDB;
        ITaskContext TaskDB;
        public HomeController(ICurrentUser currentuser,IFileContext FileDBContext,ITaskContext TaskDBContext)
        {
            Currentuser = currentuser;
            FileDB = FileDBContext;
            TaskDB = TaskDBContext;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        [Authorize(Roles = "Administrator, Employee,FrontDesk,SuperAdmin")]
        public ActionResult SystemIndex()
        {
            return View();
        }

        [Authorize]
        public ActionResult Customer()
        {


            //string currentuserName = User.Identity.GetUserName().ToString(); // For sending Customer ID code
            //ViewBag.CustomerID = currentuserName;

            string currentuserID = Currentuser.UserID;
            //ViewBag.tasks = TaskDB.Tasks.Include(t => t.Customer).Where(a => a.Customer.UserId == currentuserID).Count();
            ViewBag.CustomerID = currentuserID;// For sending Customer ID code

            int x = FileDB.GetAllFiles.Where(a => a.UserIdd == currentuserID).Count();
            ViewBag.files = x;

            return View();
        }
        public ActionResult CRM()
        {
            return RedirectToAction("Index", "Business");


        }



    }
}