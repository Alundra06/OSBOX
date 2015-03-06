using OSBOX.Common.Infrastructure;
using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;


namespace OSBOX.CRM.Controllers
{
    //Allow loggedin users only
    [AuthorizeAuthenticatedOnly]
    public class BusinessController : Controller, IBusinessController
    {
        //private OSBOXEntities BusinessDB = new OSBOXEntities();
        
        //IoC
        private IBusinessContext BusinessDB;
        private ICustomerContext CustomerDB;





        public BusinessController(IBusinessContext BusinessDBContext,ICustomerContext CustomerDBContext)
        {
            BusinessDB = BusinessDBContext;
            CustomerDB = CustomerDBContext;

        }












        // GET: /Business/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string SearchCriteria)
        {
            
            //To show a notification message
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

            var Businesses = from s in BusinessDB.GetAllBusinesses
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
               
                Businesses = Businesses.Where(s => s.Description.ToUpper().Contains(searchString.ToUpper()));
               
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Businesses = Businesses.OrderByDescending(s => s.Description);
                    break;
                case "ID_Code":
                    Businesses = Businesses.OrderBy(s => s.Customer.ID_Code);
                    break;
                case "ID_Code_desc":
                    Businesses = Businesses.OrderByDescending(s => s.Customer.ID_Code);
                    break;
                default:
                    Businesses = Businesses.OrderBy(s => s.Description);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
             //TODO i need to create the paged feature using AJAX instead of Paged
             return View(Businesses.ToPagedList(pageNumber, pageSize));
            //return View(customers.ToList());
           
          
            
        }



        // GET: /Business/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessModel business = BusinessDB.GetAllBusinesses.Where(s=>s.BusinessID ==id).FirstOrDefault();
            if (business == null)
            {
                return HttpNotFound();
            }
            
            return View(business);
        }

        // GET: /Business/Create
        public ActionResult Create( )
        {


            int CustomerID = Convert.ToInt16( TempData["CustomerID"]);
            
            if(CustomerID==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessModel businessmodel = new BusinessModel();
            businessmodel.CustomerId = CustomerID;
            businessmodel.Customer = CustomerDB.GetAllCustomers.Where(s => s.CustomerId == CustomerID).FirstOrDefault();
            businessmodel.Description = CustomerDB.GetAllCustomers.Where(s => s.CustomerId == CustomerID).FirstOrDefault().Business_Name;
            return View(businessmodel);
        }

        // POST: /Business/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="BusinessID,Description,FIN,CR,SUTA,Entity,Filing,BFile,Status,Info,Div,EFTPS,CustomerId")] BusinessModel business)
        {
            if (ModelState.IsValid)
            {
                BusinessDB.Businesses.Add(business);
                BusinessDB.Commit();
                TempData["NotificationMessage"] = "The account was created successfully!";
                return RedirectToAction("Index");
            }

            //TODO
            //ViewBag.CustomerId = new SelectList(BusinessDB.Customers, "CustomerId", "UserId", business.CustomerId);
            return View(business);
        }

        public ActionResult Search(string search)
        {

            return View(BusinessDB.Businesses.Where(x => x.Description.StartsWith(search)).ToList());
        }
        
            
        
        // GET: /Business/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessModel business = BusinessDB.GetAllBusinesses.Where(s => s.BusinessID == id).FirstOrDefault();
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }
        // POST: /Business/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessID,Description,FIN,CR,SUTA,Entity,Filing,BFile,Status,Info,Div,EFTPS,CustomerId")] BusinessModel businessmodel)
        {
            if (ModelState.IsValid)
            {
                BusinessDB.Update(businessmodel);
                TempData["NotificationMessage"] = "The account was changed successfully!";
                return RedirectToAction("Index");
            }
            return View(businessmodel);
        }

        // GET: /Business/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessModel business = BusinessDB.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }
        // POST: /Business/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessModel business = BusinessDB.Businesses.Find(id);
            BusinessDB.Businesses.Remove(business);
            BusinessDB.Commit();
            TempData["NotificationMessage"] = "The account was deleted successfully!";
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                BusinessDB.DisposeOfObject();
            }
            base.Dispose(disposing);
        }
    }
}
