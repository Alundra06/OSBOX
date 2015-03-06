using OSBOX.Common.Infrastructure;
using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
namespace OSBOX.S_chedule.Controllers
{
    //Allow loggedin users only
    [AuthorizeAuthenticatedOnly]
    public class CalendarController : Controller
    {



        private ITaskContext TaskDB;

        public CalendarController(ITaskContext TaskDBContext)
        {
            TaskDB = TaskDBContext;
        }












        [Authorize]
        public ActionResult Index()
        {
            //Being initialized in that way, scheduler will use CalendarController.Data as a the datasource and CalendarController.Save to process changes
            var scheduler = new DHXScheduler();
            scheduler.Skin = DHXScheduler.Skins.Terrace;
            scheduler.DataAction = Url.Action("Data");
            //scheduler.SaveAction = Url.Action("Save");
            scheduler.Config.readonly_form = true;
            scheduler.Config.dblclick_create = false;
            scheduler.Config.show_loading = true;
            scheduler.Config.max_month_events = 3;
            scheduler.InitialView = scheduler.Views[0].Name;

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            //return the number of tasks for this specific user

            string currentuserID = User.Identity.GetUserId().ToString();
            if (User.IsInRole("Customer"))
                ViewBag.tasks = TaskDB.Tasks.Include(t => t.Customer).Where(a => a.Customer.UserId == currentuserID).Count();
            else if (User.IsInRole("Administrator") || User.IsInRole("FrontDesk"))
                ViewBag.tasks = TaskDB.Tasks.Count();
            else
                ViewBag.tasks = TaskDB.Tasks.Include(t => t.Employee).Where(a => a.Employee.UserId == currentuserID).Count();

            return View(scheduler);
        }

        public ContentResult Data()
        {

            List<CalendarEvent> myevents = new List<CalendarEvent>();
            var taskss = TaskDB.Tasks.Include(t => t.Customer);
            if (User.IsInRole("Administrator") || User.IsInRole("FrontDesk"))
            {

            }
            else
            {
                string currentuserID = User.Identity.GetUserId().ToString();
                //myTasks.Tasks = myTasks.Tasks.Include(t=>t.Customer).Include(t => t.Employee).Include(t => t.Files).Where(a => a.Employee.UserId == currentuserID || a.Customer.UserId == currentuserID);
                taskss = TaskDB.Tasks.Include(t => t.Customer).Include(t => t.Employee).Include(t => t.Files).Where(a => a.Employee.UserId == currentuserID || a.Customer.UserId == currentuserID);
            }


            int i = 0;
            foreach (var r in taskss)
            {
                myevents.Add(new CalendarEvent(i++, r.Name, r.StartDate, r.DueDate));
            }
            int tt = myevents.Count();
            var data = new SchedulerAjaxData(myevents);

            return Content(data, "text/json");//note different content types
        }



        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);

            try
            {
                var changedEvent = (CalendarEvent)DHXEventsHelper.Bind(typeof(CalendarEvent), actionValues);



                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        //do insert
                        action.TargetId = changedEvent.id;//assign postoperational id
                        break;
                    case DataActionTypes.Delete:
                        //do delete
                        break;
                    default:// "update"                          
                        //do update
                        break;
                }
            }
            catch
            {
                action.Type = DataActionTypes.Error;
            }
            //return (ContentResult)new AjaxSaveResponse(action);
            return Content(new AjaxSaveResponse(action), "text/xml");
        }
    }
}

