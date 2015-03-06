﻿using OSBOX.Common.Controllers;
using OSBOX.Common.Infrastructure;
using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;




namespace OSBOX.S_chedule.Controllers
{
    //Allow loggedin users only
    [AuthorizeAuthenticatedOnly]

    public class TaskController : Controller
    {

        private ApplicationDbContext UserDB = new ApplicationDbContext();
        private IFileContext FileDB;
        private IFolderController Foldercontroller;
        private IFileController Filecontroller;
        private ICustomerContext CustomerDB;
        private ITaskContext TaskDB;
        private IEmployeeContext EmployeeDB;
        

        public TaskController(IFolderController Myfolders, IFileController MyFiles, ICustomerContext CustomerContext, IFileContext FileDBContext, ITaskContext TaskDBContext, IEmployeeContext EmployeeDBContext)
        {
            Foldercontroller = Myfolders;
            Filecontroller = MyFiles;
            CustomerDB = CustomerContext;
            FileDB = FileDBContext;
            TaskDB = TaskDBContext;
            EmployeeDB = EmployeeDBContext;

        }


        //this constructor is for testing purposes


        public TaskController(ITaskContext TaskDBContext,ICustomerContext CustomerDBContext,IEmployeeContext EmployeeDBContext)
        {

            TaskDB = TaskDBContext;
            CustomerDB = CustomerDBContext;
            EmployeeDB = EmployeeDBContext;

        }









        // GET: /Task/
        public ActionResult Index(string Message, string currentFilter, string sortOrder, string searchString, int? page, FormCollection form, TaskModel tt)
        {
            IQueryable<TaskModel> tasks;

            //Check this webpage for more info
            //http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.DueDateSortParm = sortOrder == "DueDate" ? "Duedate_desc" : "DueDate";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "Status_desc" : "Status";
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;

            }

            ViewBag.CurrentFilter = searchString;

            //End of parameters for paging, sorting and searching

            //The message to display
            if (Message != null)
                ViewBag.message = Message;
            else
                ViewBag.message = "All Tasks for:" + User.Identity.Name;

            //checked the role ArgumentOutOfRangeException the current user
            if (User.IsInRole("Administrator") || User.IsInRole("FrontDesk"))
            {
                tasks = TaskDB.Tasks.Include(t => t.Customer).Include(t => t.Employee).Include(t => t.Files);
            }
            else
            {
                string CurrentUserID = User.Identity.GetUserId().ToString();
                tasks = TaskDB.Tasks.Include(t => t.Customer).Include(t => t.Employee).Include(t => t.Files).Where(a => a.Employee.UserId == CurrentUserID || a.Customer.UserId == CurrentUserID);

            }
            switch (sortOrder)
            {
                case "name_desc":
                    tasks = tasks.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    tasks = tasks.OrderBy(s => s.StartDate);
                    break;
                case "date_desc":
                    tasks = tasks.OrderByDescending(s => s.StartDate);
                    break;
                case "Status":
                    tasks = tasks.OrderBy(s => s.Status);
                    break;
                case "Status_desc":
                    tasks = tasks.OrderByDescending(s => s.Status);
                    break;
                case "DueDate":
                    tasks = tasks.OrderBy(s => s.DueDate);
                    break;
                case "Duedate_desc":
                    tasks = tasks.OrderByDescending(s => s.DueDate);
                    break;
                default:
                    tasks = tasks.OrderBy(s => s.Name);
                    break;
            }

            if (Convert.ToBoolean(tt.option))
            {

                tasks = tasks.Where(a => a.Employee_ID == null);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            if (tt.Employee_ID != null)
            {
                tasks = tasks.Where(s => s.Employee_ID == tt.Employee_ID);
            }


            int pageSize = 10;
            int pageNumber = (page ?? 1);

            GetEmployees();//get the Employee first name + Last name 

            return View(tasks.ToPagedList(pageNumber, pageSize));

        }


        //Get the task for the specific user
        public JsonResult GetTasks()
        {
            var tasks = from s in TaskDB.GetAllTasks select s;
            //checked the role ArgumentOutOfRangeException the current user
            if (User.IsInRole("Administrator") || User.IsInRole("FrontDesk"))
            {
                tasks = TaskDB.GetAllTasks.Include(t => t.Customer).Include(t => t.Employee).Include(t => t.Files);
            }
            else
            {
                string currentuserID = User.Identity.GetUserId().ToString();
                tasks = TaskDB.GetAllTasks.Include(t => t.Customer).Include(t => t.Employee).Include(t => t.Files).Where(a => a.Employee.UserId == currentuserID || a.Customer.UserId == currentuserID);

            }
            return ReturnTasksforGrid(tasks);



        }




        
        //Return Tasks as JSON for the DHTMLX Grid control
        public JsonResult ReturnTasksforGrid(IEnumerable<TaskModel> FilteredTasks)
        {
           
            
            
            
            var customerdd = CustomerDB.GetAllCustomers.FirstOrDefault().CustomerId;
            var retValue = FilteredTasks.Select(
                 x => new
                 {

                     TaskName = x.Name + "^/task/Details/" + x.TasksID + "^_self",
                     Complete = x.Complete,
                     Status = x.Status,
                     Description = x.Description,
                     StartDate = x.StartDate.ToString("MM/dd/yyyy"),
                     DueDate = x.DueDate.ToString("MM/dd/yyyy"),
                     
                     //Format the Employee First and Last Name
                     Assignedto = ReturnEmployeeInformation(x.Employee_ID),
                     
                     //Format the Customer account and business name
                     Customer = ReturnCustomerInformation(x.CustomerID),
                     Update = "Update^/task/Edit/" + x.TasksID + "javascript:alert(1);",
                     Delete = "Delete^/task/Delete/" + x.TasksID + "^_self"
                               

                 }).ToList();
            
            JsonResult retVal = new JsonResult();
            retVal.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            retVal.Data = retValue;
            //return serializer.Serialize(retValue);
            return retVal;
        }

        private string ReturnEmployeeInformation(int? EmployeeID)
        {
            EmployeeModel LocalEmployeeModel = EmployeeDB.GetAllEmployees.Where(m => m.Employee_ID == EmployeeID).FirstOrDefault();

            if (LocalEmployeeModel != null)
                return (LocalEmployeeModel.FirstName ?? "").ToString() + " - "
                        + (LocalEmployeeModel.LastName ?? "").ToString();
            else
                return "";
        }

        private string ReturnCustomerInformation(int? CustomerID)
        {
            CustomerModel LocalCustomerModel = CustomerDB.GetAllCustomers.Where(m => m.CustomerId == CustomerID).FirstOrDefault();

            if (LocalCustomerModel != null)
                return (LocalCustomerModel.ID_Code ?? "").ToString() + " - "
                        + (LocalCustomerModel.Business_Name ?? "").ToString();
            else
                return "";
        }












        public ActionResult Index3()
        {

            return View();
        }
        public string Index2(string id)
        {

            string expected = TaskDB.GetAllTasks.Where(m => m.TasksID == id).First().TasksID;
            ViewBag.test = "Free Test";
            return expected;
        }


        public ViewResult Index5()
        {
            ViewBag.Test = "Test Free";
            return View();
        }















        // GET: /Task/Details/5
        public ActionResult Details(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskModel TaskModel = TaskDB.Tasks.Find(id);
            if (TaskModel == null)
            {
                return HttpNotFound();
            }

            return View(TaskModel);
            //return RedirectToAction("Name of the action","Name of the controller");
        }

        // GET: /Task/Create
        public ActionResult Create()
        {
            GetCustomers();//get the customer first name + Last name + Business name
            GetEmployees();//get the Employee first name + Last name 
            GetTasksStatus();
            GetPriorities();

            return View();
        }


        private IEnumerable<SelectListItem> GetTasksStatus()
        {
            IEnumerable<SelectListItem> TaskS;
            TaskS = new[]
                  {
                    new SelectListItem { Value = "Not Started", Text = "Not Started" },
                    new SelectListItem { Value = "In Progress", Text = "In Progress" },
                    new SelectListItem { Value = "Completed", Text = "Completed" },
                    new SelectListItem { Value = "Deferred", Text = "Deferred" },
                    new SelectListItem { Value = "Waiting on someone else", Text = "Waiting on someone else" }
                  };
            ViewBag.TasksStatus = TaskS;
            return TaskS;

        }

        private void GetFiles(string id)
        {

            TempData["Filemodel"] = FileDB.Files.Where(b => b.TasksID == id);
        }



        private void GetPriorities()
        {
            IEnumerable<SelectListItem> Prios;
            Prios = new[]
                  {
                    new SelectListItem { Value = "1", Text = "High" },
                    new SelectListItem { Value = "2", Text = "Normal" },
                    new SelectListItem { Value = "3", Text = "Low" }
                  };
            ViewBag.TasksPriorities = Prios;

        }


        private void GetEmployees()
        {
            IEnumerable<SelectListItem> Employeess =
            (from Employees in TaskDB.EmployeeModels.ToArray()
             select new SelectListItem
             {
                 Text = Employees.FirstName + " " + Employees.LastName,
                 Value = Employees.Employee_ID.ToString()
             }).ToArray();

            var Templist = Employeess.ToList();
            Templist.Add(new SelectListItem { Text = "---", Value = "", Selected = true });
            ViewBag.Employees = Templist.ToArray();



        }



        public void GetCustomers()
        {
            IEnumerable<SelectListItem> customers =
             (from Customers in TaskDB.CustomerModels.ToArray().OrderBy(s => s.Business_Name)

              select new SelectListItem
              {
                  Text = Customers.Business_Name + ", " + UserDB.Users.Find(Customers.UserId).UserName + ", " + Customers.First_Name + " " + Customers.Last_Name,
                  Value = Customers.CustomerId.ToString()
              }).ToArray();
            var Templist = customers.ToList();
            Templist.Add(new SelectListItem { Text = "---", Value = "", Selected = true });
            ViewBag.Customers = Templist.ToArray();
        }

        // POST: /Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TasksID,Name,StartDate,DueDate,Complete,Description,Priority,Status,AssignementDate,Employee_ID,CustomerID")] TaskModel Taskmodel, FormCollection form, HttpPostedFileBase fileUpload)
        {

            if (ModelState.IsValid)
            {

                Taskmodel.TasksID = Guid.NewGuid().ToString();
                Taskmodel.CreationDate = DateTime.Now;

                TaskDB.Tasks.Add(Taskmodel);
                TaskDB.Commit();

                //Check if the user uploaded a file or not
                if (fileUpload != null)
                {
                  
                  

                    //////////////////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////
                    /////Create folders and upload the file////////////////
                    //////////////////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////

                    //string CurrentUserID = User.Identity.GetUserId();
                    //string ParentFolder = "dc4678b6-b3f5-4b34-b352-d3acac1f1f8a";
                    // string tempLocation = Server.MapPath(@"\Uploads\" + fileUpload.FileName);

                    //FileModel ThisFile = Filecontroller.UploadFileToServer(fileUpload, ParentFolder, null, TaskModel.TasksID);
                    //Filecontroller.UploadFileToDropBox(ThisFile.FolderPath, tempLocation);

                    
                    String TaskFolderID = Foldercontroller.GetIDForSystemFolder("Tasks");
                    Filecontroller.Upload_File_Complete(fileUpload, TaskFolderID, Taskmodel.TasksID);
                    
                }
                return RedirectToAction("index");
            }

            ViewBag.CustomerID = new SelectList(TaskDB.CustomerModels, "CustomerId", "CustomerId", Taskmodel.CustomerID);
            ViewBag.EmployeeID = new SelectList(TaskDB.EmployeeModels, "Employee_Id", "Employee_Id", Taskmodel.Employee_ID);

            return View(Taskmodel);
        }



        private void CreateFileModel(TaskModel TaskModel, FormCollection form, HttpPostedFileBase fileUpload)
        {
            var Userid = (from d in CustomerDB.Customers where d.CustomerId == TaskModel.CustomerID select d).FirstOrDefault().UserId;
            var UserName = (from d in UserDB.Users where d.Id == Userid select d).FirstOrDefault().UserName;


            //return RedirectToAction("Index");
            FileModel filemodel = new FileModel()
            {
                UserIdd = Userid,
                Name = Path.GetFileName(fileUpload.FileName),
                UploadDate = DateTime.Now,
                FilePath = Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(fileUpload.FileName)),
                TasksID = TaskModel.TasksID,
                FolderPath = UserName + "/" + TaskModel.Name

            };
            TempData["Filemodel"] = filemodel;

        }

        // GET: /Task/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskModel TaskModel = TaskDB.Tasks.Find(id);
            if (TaskModel == null)
            {
                return HttpNotFound();
            }
            GetCustomers();
            GetEmployees();
            return View(TaskModel);
        }

        // POST: /Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TasksID,Name,StartDate,DueDate,Complete,Description,Priority,Status,AssignementDate,CreationDate,Employee_ID,CustomerID,CompletionDate")] TaskModel Taskmodel, FormCollection form, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {



                //TaskDB.Entry(TaskModel).State = EntityState.Modified;
                //TaskDB.Commit();
                TaskDB.Update(Taskmodel);
                //Check if the user uploaded a file or not
                if (fileUpload != null)
                {
                    if (Taskmodel.CustomerID == null)
                    { return RedirectToAction("index"); }
                    else
                    {
                        String TaskFolderID = Foldercontroller.GetIDForSystemFolder("Tasks");
                        //Filecontroller.UploadFileToServer(fileUpload, TaskFolderID, Taskmodel.CustomerID.ToString(), Taskmodel.TasksID);
                        Filecontroller.Upload_File_Complete(fileUpload, TaskFolderID,Taskmodel.TasksID);
                        //CreateFileModel(Taskmodel, form, fileUpload);
                        ////Upload the file to it's temporary location
                        //fileUpload.SaveAs(Path.Combine(Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(fileUpload.FileName))));
                        //return RedirectToAction("UploadToDropBox", "File", new { taction = "index", tcontroller = "Task" });
                        //return View("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            GetCustomers();
            GetEmployees();
            return View(Taskmodel);
        }

        // GET: /Task/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskModel TaskModel = TaskDB.Tasks.Find(id);
            if (TaskModel == null)
            {
                return HttpNotFound();
            }
            GetCustomers();
            GetEmployees();
            return View(TaskModel);
        }

        // POST: /Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TaskModel TaskModel = TaskDB.Tasks.Find(id);
            TaskDB.Tasks.Remove(TaskModel);
            TaskDB.Commit();
            //Delete the associated files with the task
            Filecontroller.DeleteFilesOnATaskFolderComplete(TaskModel.TasksID);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TaskDB.DisposeOfObject();
            }
            base.Dispose(disposing);
        }


        public ActionResult Datagrid()
        {
            return View();
        }

        //public string GetTaskName(String TaskID)
        //{
        //    string ss = repository.GetTaskNameByID(TaskID);
        //    return repository.GetTaskNameByID(TaskID);

        //}










    }
}

