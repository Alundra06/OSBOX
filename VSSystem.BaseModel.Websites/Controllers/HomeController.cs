using AKCPA.Data.DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;


//namespace scheduleMVC.Controllers
namespace VSSystem.BaseModel.WebSites.Controllers
{
    public class HomeController : Controller
    {
        private ICustomerContext CustomerDB ;
        private ITaskContext TaskDB;
        private IFileContext FileDB;

        public HomeController(ITaskContext TaskDBContext,ICustomerContext CustomerDBContext,IFileContext FileDBContext)
        {
            TaskDB = TaskDBContext;
            CustomerDB = CustomerDBContext;
            FileDB = FileDBContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Albert T. Kim, CPA, PA is located in Catonsville, MD, just minutes from I695 & I70.  We are a full service accounting firm specializing in small to medium sized businesses.  If you need assistance with your accounting or bookkeeping, please contact us for a consultation.";
            return View();
        }



        public String GetCustomers()
        {
            var customers = from s in CustomerDB.Customers.Where(a=>a.CustomerId<=20)
                            select s;


            var retValue = customers.Select(
                x => new
                {
                    id = x.CustomerId,
                    fname = x.First_Name,
                    lname = x.Last_Name
                  
                  
                }).ToArray();
            //XNode node = JsonConvert.DeserializeXNode("{\"root\":" + JsonConvert.SerializeObject(retValue) + "}", "root");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            
         
            
          // return Json(new { Data = retValue }, JsonRequestBehavior.AllowGet);
            
            //return Json(new { Data = retValue }, JsonRequestBehavior.AllowGet);
            return serializer.Serialize(retValue);
            //return JsonConvert.SerializeObject(retValue);

            //return Json(new { data = JsonConvert.SerializeObject(retValue) }, JsonRequestBehavior.AllowGet);
           



        }

        
      
        public ActionResult Contact()
        {
            ViewBag.Message = "Albert T. Kim, CPA, PA";
            return View();
        }
        
       
        
         [Authorize(Roles = "Administrator, Employee,FrontDesk,SuperAdmin")]
        public ActionResult SystemIndex()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public ActionResult Customer()
        {

           
           
        
            string currentuserID = User.Identity.GetUserId().ToString();
            ViewBag.tasks = TaskDB.Tasks.Include(t => t.Customer).Where(a =>  a.Customer.UserId == currentuserID).Count();
            ViewBag.files =FileDB.Files.Where(a => a.UserIdd == currentuserID).Count();

            return View();
        }
        public ActionResult CRM()
        {
            return RedirectToAction("Index", "Business");


        }
        
    }
}