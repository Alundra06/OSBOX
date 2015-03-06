using OSBOX.Common.Infrastructure;
using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OSBOX.Common.Controllers
{
     [AuthorizeAuthenticatedOnly]
    public class AddressController:Controller
    {

        private readonly IAddressContext AddressDB;
        private readonly ICustomerContext CustomerDB;
        
        public AddressController(IAddressContext AddressDBContext,ICustomerContext CustomerDBContext)
        {
            AddressDB = AddressDBContext;
            CustomerDB = CustomerDBContext;
        }
        
      
         
        public ActionResult Index(int? CustomerId)
        {
            IQueryable<AddressModel> am=AddressDB.GetAllAddresses;
            if(CustomerId!=null)
            {
                am = am.Where(s => s.CustomerId == CustomerId);
            }
         
            
            ViewBag.CustomersList = new SelectList(CustomerDB.Customers, "CustomerId", "ID_Code");
            return View(am);
        }




        // GET: /Business/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressModel address = AddressDB.GetAllAddresses.Where(s => s.AddressId == id).FirstOrDefault();
            if (address == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatesList = new SelectList(States, "Value", "Text");
            return View(address);
        }
       
         
         
         
         
         
         
         
    
    
    
    
    
    
    
    
    
    
    
    
    
    
   


         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         // POST: /Business/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddressModel addressmodel)
        {
            if (ModelState.IsValid)
            {
                AddressDB.Update(addressmodel);
                TempData["NotificationMessage"] = "The address was changed successfully!";
                return RedirectToAction("Index");
            }
            return View(addressmodel);
        }

        // GET: /Business/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressModel address= AddressDB.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }
        // POST: /Business/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AddressModel address = AddressDB.Addresses.Find(id);
            AddressDB.Addresses.Remove(address);
            AddressDB.Commit();
            TempData["NotificationMessage"] = "The address was deleted successfully!";
            return RedirectToAction("Index");
        }


        public ActionResult Create()
        {

            //AddressModel am = new AddressModel();
            //GetStates(am);
            ViewBag.CustomersList = new SelectList(CustomerDB.Customers, "CustomerId", "ID_Code");
            ViewBag.StatesList = new SelectList(States, "Value", "Text");
            return View();
            

        }

        private static List<SelectListItem> States = new List<SelectListItem>()
    {
        new SelectListItem() {Text="Alabama", Value="AL"},
        new SelectListItem() { Text="Alaska", Value="AK"},
        new SelectListItem() { Text="Arizona", Value="AZ"},
        new SelectListItem() { Text="Arkansas", Value="AR"},
        new SelectListItem() { Text="California", Value="CA"},
        new SelectListItem() { Text="Colorado", Value="CO"},
        new SelectListItem() { Text="Connecticut", Value="CT"},
        new SelectListItem() { Text="District of Columbia", Value="DC"},
        new SelectListItem() { Text="Delaware", Value="DE"},
        new SelectListItem() { Text="Florida", Value="FL"},
        new SelectListItem() { Text="Georgia", Value="GA"},
        new SelectListItem() { Text="Hawaii", Value="HI"},
        new SelectListItem() { Text="Idaho", Value="ID"},
        new SelectListItem() { Text="Illinois", Value="IL"},
        new SelectListItem() { Text="Indiana", Value="IN"},
        new SelectListItem() { Text="Iowa", Value="IA"},
        new SelectListItem() { Text="Kansas", Value="KS"},
        new SelectListItem() { Text="Kentucky", Value="KY"},
        new SelectListItem() { Text="Louisiana", Value="LA"},
        new SelectListItem() { Text="Maine", Value="ME"},
        new SelectListItem() { Text="Maryland", Value="MD"},
        new SelectListItem() { Text="Massachusetts", Value="MA"},
        new SelectListItem() { Text="Michigan", Value="MI"},
        new SelectListItem() { Text="Minnesota", Value="MN"},
        new SelectListItem() { Text="Mississippi", Value="MS"},
        new SelectListItem() { Text="Missouri", Value="MO"},
        new SelectListItem() { Text="Montana", Value="MT"},
        new SelectListItem() { Text="Nebraska", Value="NE"},
        new SelectListItem() { Text="Nevada", Value="NV"},
        new SelectListItem() { Text="New Hampshire", Value="NH"},
        new SelectListItem() { Text="New Jersey", Value="NJ"},
        new SelectListItem() { Text="New Mexico", Value="NM"},
        new SelectListItem() { Text="New York", Value="NY"},
        new SelectListItem() { Text="North Carolina", Value="NC"},
        new SelectListItem() { Text="North Dakota", Value="ND"},
        new SelectListItem() { Text="Ohio", Value="OH"},
        new SelectListItem() { Text="Oklahoma", Value="OK"},
        new SelectListItem() { Text="Oregon", Value="OR"},
        new SelectListItem() { Text="Pennsylvania", Value="PA"},
        new SelectListItem() { Text="Rhode Island", Value="RI"},
        new SelectListItem() { Text="South Carolina", Value="SC"},
        new SelectListItem() { Text="South Dakota", Value="SD"},
        new SelectListItem() { Text="Tennessee", Value="TN"},
        new SelectListItem() { Text="Texas", Value="TX"},
        new SelectListItem() { Text="Utah", Value="UT"},
        new SelectListItem() { Text="Vermont", Value="VT"},
        new SelectListItem() { Text="Virginia", Value="VA"},
        new SelectListItem() { Text="Washington", Value="WA"},
        new SelectListItem() { Text="West Virginia", Value="WV"},
        new SelectListItem() { Text="Wisconsin", Value="WI"},
        new SelectListItem() { Text="Wyoming", Value="WY"}
    };
        // POST: /Business/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddressModel address)
        {
            if (ModelState.IsValid)
            {
                AddressDB.Addresses.Add(address);
                AddressDB.Commit();
                TempData["NotificationMessage"] = "The addresswas created successfully!";
                return RedirectToAction("Index");
            }

            //TODO
            //ViewBag.CustomerId = new SelectList(BusinessDB.Customers, "CustomerId", "UserId", business.CustomerId);
            return View(address);
        }












    }
}
