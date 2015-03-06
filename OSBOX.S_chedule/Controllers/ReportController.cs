﻿using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace OSBOX.S_chedule.Controllers
{
    //Allow loggedin users only
    [Authorize]
    public class ReportController : Controller
    {
        private ITaskContext TaskDB;

        public ReportController(ITaskContext TaskDBContext)
        {
            TaskDB = TaskDBContext;
        }




        //
        // GET: /Report/
        [Authorize(Roles = "Administrator, Employee,FrontDesk,SuperAdmin")]
        public ActionResult Index()
        {

            IQueryable<TaskModel> tasks;

            tasks = TaskDB.Tasks.Include(t => t.Customer).Include(t => t.Employee).Include(t => t.Files);

            //Total number of tasks
            ViewBag.TNTasks = tasks.Count();

            //Total number of finished tasks
            ViewBag.TNFTasks = tasks.Where(a => a.Status == "3").Count();

            //Total number of unfinished tasks
            ViewBag.TNUTasks = tasks.Where(a => a.Status != "3").Count();

            //Total number of finished tasks on time
            ViewBag.TNFTTasks = tasks.Where(a => a.DueDate <= a.CompletionDate).Count();

            //Employee with most active tasks
            string ss = (tasks.OrderByDescending(t => t.Employee_ID).Take(1)).Single().Employee.FirstName + " " +
                (tasks.OrderByDescending(t => t.Employee_ID).Take(1)).Single().Employee.LastName;
            ViewBag.EmpActiveTasks = ss;

            //Employee with most finished tasks

            if (ViewBag.TNUTasks > 0)
            {
                ss = (tasks.Where(a => a.Status == "3").OrderByDescending(t => t.Employee_ID).Take(1)).Single().Employee.FirstName + " " +
                (tasks.Where(a => a.Status == "3").OrderByDescending(t => t.Employee_ID).Take(1)).Single().Employee.LastName;
                ViewBag.EmpFinishedTasks = ss;
            }

            //Employee with most finished tasks on time

            if (ViewBag.TNFTTasks > 0)
            {
                ss = (tasks.Where(a => a.DueDate <= a.CompletionDate).OrderByDescending(t => t.Employee_ID).Take(1)).Single().Employee.FirstName + " " +
                (tasks.Where(a => a.DueDate <= a.CompletionDate).OrderByDescending(t => t.Employee_ID).Take(1)).Single().Employee.LastName;
                ViewBag.EmpFinishedTasksontime = ss;
            }





            return View();
        }
    }
}