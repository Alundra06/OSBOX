using OSBOX.Common.Infrastructure;
using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using OSBOX.Data.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;



namespace OSBOX.Common.Controllers
{
    //Allow loggedin users only
    [AuthorizeAuthenticatedOnly]
    //Allow Users with the roles: Administrator or Employee only
    [AuthorizeEmployeesOnly(Roles = "Administrator,Employee")]
    public class CustomerController : Controller, ICustomerController
    {
        
        
        
        private ICustomerContext CustomerDB ;
        private ApplicationDbContext UserDB = new ApplicationDbContext();
        private ITaskContext TaskDB ;
        private IFileContext FileDB ;
        private IFolderController Foldercontroller;



        ////Property injection
        ////http://ninject.codeplex.com/wikipage?title=Injection%20Patterns
        [Inject]
        public IFolderController FoldercontrollerEX
        {
            get { return Foldercontroller; }
            set { Foldercontroller = value; }
        }



        public CustomerController(ITaskContext TaskDBContext,IFileContext FileDBContext, ICustomerContext CustomerDBContext)
        {
            TaskDB = TaskDBContext;
            FileDB = FileDBContext;
            CustomerDB = CustomerDBContext;
            //Foldercontroller = foldercontroller;

        }

    
        public IQueryable<CustomerModel> GetAllCustomers()
        {
            return CustomerDB.GetAllCustomers;
        }






      //   [Authorize(Roles = "Administrator")]
         
        // GET: /Customer/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {


            if (TempData["NotificationMessage"] != null)
            {
                ViewBag.Notificationmessage = TempData["NotificationMessage"];
            }
            
            
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDCodeSortParm = sortOrder == "ID_Code" ? "ID_Code_desc" : "ID_Code";
            
            //For pagination
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var customers = from s in CustomerDB.Customers
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.Business_Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(s => s.Business_Name);
                    break;
                case "ID_Code":
                    customers = customers.OrderBy(s => s.ID_Code);
                    break;
                case "ID_Code_desc":
                    customers = customers.OrderByDescending(s => s.ID_Code);
                    break;
                default:
                    customers = customers.OrderBy(s => s.Business_Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
             //TODO i need to create the paged feature using AJAX instead of Paged
             return View(customers.ToPagedList(pageNumber, pageSize));
            //return View(customers.ToList());
           
          
            
        }

        // GET: /Customer/Details/5
         [Authorize(Roles = "Administrator")]
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerModel customermodel = CustomerDB.Customers.Find(id);
            if (customermodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.username = UserDB.Users.Where(a => a.Id == customermodel.UserId).ToList().FirstOrDefault().UserName;
            return View(customermodel);
        }




        private void GetUsers()
        {
            User.IsInRole("");
            ViewBag.Users =
             (from Users
                  in UserDB.Users.ToArray() 
            // where Roles.GetRolesForUser(Users.UserName).FirstOrDefault().ToString() == "Customer"
            // where (Roles.IsUserInRole(Users.UserName,"Customer") == true)
              select new SelectListItem
              {
                  Text = Users.UserName,
                  Value = Users.Id
              }).ToArray();
        }




        public JsonResult AutoCompleteAccountID(string term)
        {

            var ListOfAccountIDs = CustomerDB.GetAllCustomers.Where(s => s.ID_Code.StartsWith(term)).Select(r => new { label = r.ID_Code });
            //return Json(ListOfAccountIDs, JsonRequestBehavior.AllowGet);
            
            
            JsonResult retVal = new JsonResult();

            retVal.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            retVal.Data = ListOfAccountIDs;

            return retVal;
            //get this code from the videos on MVC asp.net

        }
        
        
        
        
        
        
        
        
        public IEnumerable<SelectListItem> GetCustomersAccounts()
        {
            IEnumerable<SelectListItem> customers =
             (from Customers in CustomerDB.GetAllCustomers.ToArray().OrderBy(s => s.CustomerId)

              select new SelectListItem
              {
                 // Text = Customers.Business_Name + ", " + UserDB.Users.Find(Customers.UserId).UserName + ", " + Customers.First_Name + " " + Customers.Last_Name,
                 //Text = Customers.Customer_Account,
                  Text = Customers.ID_Code + " - " +Customers.Business_Name,
                  Value = Customers.CustomerId.ToString()
              }).ToArray();
            var Templist = customers.ToList();
            Templist.Add(new SelectListItem { Text = "Please select a Customer", Value = "", Selected = true });
            //ViewBag.Customers = Templist.ToArray();
            return Templist.ToArray();
        }

       //TODO I need to look at this closer
        public string GetCustomerAccountByID(int CustomerID)
        {
            string UserID = CustomerDB.GetAllCustomers.Where(s => s.CustomerId == CustomerID).First().UserId;
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindById(UserID);
            var FolderName = user.UserName;
            return FolderName;
        }

        public int GetCustomerIDByAccount(string CustomerAccount)
       {
           return CustomerDB.GetAllCustomers.Where(s => s.ID_Code == CustomerAccount).First().CustomerId;
       }


         [Authorize(Roles = "Administrator")]
        
        // GET: /Customer/Create
        public ActionResult Create()
        {
            GetUsers();
            return View();
        }

        // POST: /Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CustomerId,UserId,Name_Prefix,Last_Name,Middle_name,First_Name,Business_Name,LegalName,Phone_Number,Cell_Phone,Fax,Email,Customer_Account")] CustomerModel customermodel)
        {
            if (ModelState.IsValid)
            {
                CustomerDB.Customers.Add(customermodel);
                CustomerDB.Commit();
                return RedirectToAction("Index");
            }

            return View(customermodel);
        }


         [Authorize(Roles = "Administrator")]
        // GET: /Customer/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerModel customermodel = CustomerDB.Customers.Find(id);
            if (customermodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.username = UserDB.Users.Where(a => a.Id == customermodel.UserId).ToList().FirstOrDefault().UserName;
            return View(customermodel);
        }

        // POST: /Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult Edit( CustomerModel customermodel)
        {
            if (ModelState.IsValid)
            {
                CustomerDB.Update(customermodel);
                TempData["NotificationMessage"] = "The account was changed successfully!";
                return RedirectToAction("Index");
            }
            return View(customermodel);
        }

        [Authorize(Roles = "Administrator")]
        // GET: /Employee/ResetPassowrd/5
        public ActionResult ResetPassword(int CustomerID)
        {


            @ViewBag.ID_Code = CustomerDB.GetAllCustomers.Where(s => s.CustomerId == CustomerID).First().ID_Code;
            @ViewBag.CustomerID = CustomerID;
            return View();
        }




        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        // [Authorize(Roles = "Administrator")]
        public ActionResult ResetPassword(ResetCustomerPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                string UserID = CustomerDB.GetAllCustomers.FirstOrDefault(u => u.CustomerId == model.CustomerID).UserId;
                userManager.RemovePassword(UserID);
                userManager.AddPassword(UserID, model.Password);
                
            }

            TempData["NotificationMessage"] = "The password was changed successfully!";
            return RedirectToAction("Index");
            
        }



















        
        //A method to reset the password for all customers to "password"
        public ActionResult ResetAllPasswords()
        {

            UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            foreach (var row in CustomerDB.Customers)
            {
                string UserID = row.UserId;
                userManager.RemovePassword(UserID);
                userManager.AddPassword(UserID, "password");
            }
            return View();
        }



         [Authorize(Roles = "Administrator")]
        // GET: /Customer/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string CustomerID = CustomerDB.GetAllCustomers.Where(s => s.CustomerId == id).First().UserId;
            int numberOfActiveTasks = TaskDB.Tasks.Include(t => t.Customer).Where(a => a.CustomerID == id && a.Status != "Completed").Count();
            int numberOfFiles = FileDB.Files.Where(a => a.UserIdd == CustomerID).Count();
            
            //Check if the customer has active tasks or files associated with his/her account
             if (numberOfActiveTasks > 0 || numberOfFiles > 0)
            {
                ViewBag.numberOfActiveTasks = numberOfActiveTasks;
                ViewBag.FullName = CustomerDB.Customers.Find(id).First_Name + " " + CustomerDB.Customers.Find(id).Last_Name;
                ViewBag.numberOfFiles = numberOfFiles;
                return View("DeleteError");
            }
            //If not then Proceed with the deletion
             else
            {

                CustomerModel customermodel = CustomerDB.Customers.Find(id);
                if (customermodel == null)
                {
                    return HttpNotFound();
                }
                ViewBag.username = UserDB.Users.Where(a => a.Id == customermodel.UserId).ToList().FirstOrDefault().UserName;
                return View(customermodel);
            }
        }

        // POST: /Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //get the user ID for the ASPNETUSERS to be deleted
            string UserID = CustomerDB.Customers.FirstOrDefault(u => u.CustomerId== id).UserId;
            
            CustomerModel customermodel = CustomerDB.Customers.Find(id);
            
            //Delete associated folders for the user
           
            //Get an instance of the user model to delete.
            var user = UserDB.Users.FirstOrDefault(u => u.Id == UserID);
            
           
           
            if (user != null)
            {
                string FolderName = user.UserName;

                if  (Foldercontroller.GetFoldersIDFromCustomerAccount(FolderName)!= null)
                {
                string FolderID = Foldercontroller.GetFoldersIDFromCustomerAccount(FolderName);
                Foldercontroller.DeleteFolderFromDropBox(FolderID);
                Foldercontroller.DeleteFolderFromDataBase(FolderID);
                }

                CustomerDB.Customers.Remove(customermodel);
                CustomerDB.Commit();
                
                //Then delete the account from the ASPTNETUSER table
                UserDB.Users.Remove(user);
                UserDB.SaveChanges();
            }
            TempData["NotificationMessage"] = "The account was successfully deleted";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CustomerDB.DisposeOfObject();
            }
            base.Dispose(disposing);
        }
    }
}









