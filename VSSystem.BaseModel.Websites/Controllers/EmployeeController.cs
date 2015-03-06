using AKCPA.Data.DAL;
using AKCPA.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace VSSystem.BaseModel.WebSites.Controllers
{

    public class EmployeeController : Controller
    {
       // private EmployeeContext db = new EmployeeContext();
        private ApplicationDbContext UserDB = new ApplicationDbContext();
        private ITaskContext TaskDB;
        private IEmployeeContext EmployeeDB;

        public EmployeeController(ITaskContext TaskDBContext,IEmployeeContext EmployeeDBContext)
        {
            TaskDB = TaskDBContext;
            EmployeeDB = EmployeeDBContext;
        }
        
        
        
        
        
        
        
        
        
        
        private string userID { get; set; }
         
        // GET: /Employee/
        public ActionResult Index(string sortOrder)
        {

            if (User.Identity.IsAuthenticated)
            {

                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                var employees = from s in EmployeeDB.GetAllEmployees
                               select s;
                switch (sortOrder)
                {
                    case "name_desc":
                        employees = employees.OrderByDescending(s => s.LastName);
                        break;
                    case "Date":
                        employees = employees.OrderBy(s => s.Hire_Date);
                        break;
                    case "date_desc":
                        employees = employees.OrderByDescending(s => s.Hire_Date);
                        break;
                    default:
                        employees = employees.OrderBy(s => s.LastName);
                        break;
                }
                
                
                return View(employees.ToList());
            }
            else
            {
                return View("EmployeeLogin");
            }
        }
        [Authorize(Roles = "Administrator")]
        // GET: /Employee/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeModel employeemodel = EmployeeDB.Employees.Find(id);
            if (employeemodel == null)
            {
                return HttpNotFound();
            }
            GetUserName(id);
            return View(employeemodel);
        }

















        [Authorize(Roles = "Administrator")]
        // GET: /Employee/ResetPassowrd/5
        public ActionResult ResetPassword(int id)
        {

            UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            string UserID = EmployeeDB.Employees.FirstOrDefault(u => u.Employee_ID == id).UserId;
            string UserName = UserDB.Users.Find(UserID).UserName;
            string FullName = EmployeeDB.Employees.Find(id).FirstName + " " + EmployeeDB.Employees.Find(id).LastName;
            userManager.RemovePassword(UserID);
            userManager.AddPassword(UserID, "password");
            @ViewBag.FullName = FullName;
            @ViewBag.UserName = UserName;
            return View();
        }
        
        [Authorize(Roles = "Administrator")]
        // GET: /Employee/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeModel employeemodel = EmployeeDB.Employees.Find(id);
            if (employeemodel == null)
            {
                return HttpNotFound();
            }


            GetUserName(id);
            
            
            return View(employeemodel);
        }


        private void GetUserName(int id)
        {
            string UserID = EmployeeDB.Employees.FirstOrDefault(u => u.Employee_ID == id).UserId;
            ViewBag.UserName = UserDB.Users.FirstOrDefault(u=>u.Id == UserID).UserName;
        }


        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Employee_ID,FirstName,LastName,Salary_Type,Manager_ID,Title,Hire_Date,Salary,Vacation_Hrs,SickLeave_Hrs,UserId")] EmployeeModel employeemodel)
        {

            //TODO

            if (ModelState.IsValid)
            {
                employeemodel.Modified_Date = DateTime.Now;
                //EmployeeDB.Entry(employeemodel).State = EntityState.Modified;
                EmployeeDB.Commit();
                return RedirectToAction("Index");
            }
            return View(employeemodel);
        }


        [Authorize(Roles = "Administrator")]
        // GET: /Employee/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Check if the employee has active tasks or not before deleting the account.
            int numberOfActiveTasks = TaskDB.Tasks.Include(t => t.Employee).Where(a => a.Employee.Employee_ID == id && a.Status != "Completed").Count();
            if (numberOfActiveTasks > 0)
            {
                ViewBag.numberOfActiveTasks = numberOfActiveTasks;
                ViewBag.FullName = EmployeeDB.Employees.Find(id).FirstName + " " + EmployeeDB.Employees.Find(id).LastName;
                return View("DeleteError");
            }
            else // if the employee doesnt have any active tasks
            {
                EmployeeModel employeemodel = EmployeeDB.Employees.Find(id);
                if (employeemodel == null)
                {
                    return HttpNotFound();
                }
                GetUserName(id);
                return View(employeemodel);
            }
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string UserID = EmployeeDB.Employees.FirstOrDefault(u => u.Employee_ID == id).UserId;

            EmployeeModel employeemodel = EmployeeDB.Employees.Find(id);
            //Delete the employee record first from the employee table
            EmployeeDB.Employees.Remove(employeemodel);
            EmployeeDB.Commit();
            var user = UserDB.Users.FirstOrDefault(u => u.Id == UserID);
            if (user != null)
            {
                //Then delete the account from the ASTNETUSER table
                UserDB.Users.Remove(user);
                UserDB.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EmployeeDB.DisposeOfObject();
            }
            base.Dispose(disposing);
        }
    }
}
