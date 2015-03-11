using OSBOX.Common.Infrastructure;
using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using PagedList;


namespace OSBOX.CRM.Controllers
{
    //Allow loggedin users only
    [AuthorizeAuthenticatedOnly]
    public class InvoiceController : Controller, OSBOX.CRM.Controllers.IInvoiceController
    {
        //IoC
        private IInvoiceContext InvoiceDB;
        private ICustomerContext CustomerDB;
        private IPaymentContext PaymentDB;
        private IServiceTypeContext ServiceTypeDB;
        private IAddressContext AddressDB;


        public InvoiceController(IInvoiceContext InvoiceDBContext,ICustomerContext CustomerDBContext,IPaymentContext PaymentDBContext,IServiceTypeContext ServiceTypeDBContext, IAddressContext AddressDBContext)
        {
            InvoiceDB = InvoiceDBContext;
            CustomerDB = CustomerDBContext;
            PaymentDB = PaymentDBContext;
            ServiceTypeDB = ServiceTypeDBContext;
            AddressDB = AddressDBContext;

        }

        // GET: /Invoice/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
           
            if (TempData["NotificationMessage"] != null)
            {
                ViewBag.Notificationmessage = TempData["NotificationMessage"];
            }


            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDCodeSortParm = sortOrder == "IDCode" ? "IDCode_desc" : "IDCode";
            ViewBag.InvoiceDateSortParm = sortOrder == "InvoiceDate" ? "InvoiceDate_desc" : "InvoiceDate";
            ViewBag.InvoiceAmountSortParm = sortOrder == "InvoiceAmount" ? "InvoiceAmount_desc" : "InvoiceAmount";
            ViewBag.InvoiceDueAmountSortParm = sortOrder == "InvoiceDueAmount" ? "InvoiceDueAmount_desc" : "InvoiceDueAmount";
            ViewBag.InvoiceDueDateSortParm = sortOrder == "InvoiceDueDate" ? "InvoiceDueDate_desc" : "InvoiceDueDate";
            ViewBag.InvoicePaymentTermSortParm = sortOrder == "InvoicePaymentTerm" ? "InvoicePaymentTerm_desc" : "InvoicePaymentTerm";
            ViewBag.InvoiceServiceTypeSortParm = sortOrder == "InvoiceServiceType" ? "InvoiceServiceType_desc" : "InvoiceServiceType";

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

            var invoices = from s in InvoiceDB.GetAllInvoices
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                invoices = invoices.Where(s => s.Invoice_Number.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    invoices = invoices.OrderByDescending(s => s.Customer.Business_Name);
                    break;
                case "ID_Code":
                    invoices = invoices.OrderBy(s => s.Customer.ID_Code);
                    break;
                case "ID_Code_desc":
                    invoices = invoices.OrderByDescending(s => s.Customer.ID_Code);
                    break;
                case "InvoiceDate":
                    invoices = invoices.OrderBy(s => s.Invoice_Date);
                    break;
                case "InvoiceDate_desc":
                    invoices = invoices.OrderByDescending(s => s.Invoice_Date);
                    break;
                case "InvoiceAmount":
                    invoices = invoices.OrderBy(s => s.Invoice_Amount);
                    break;
                case "InvoiceAmount_desc":
                    invoices = invoices.OrderByDescending(s => s.Invoice_Amount);
                    break;
                case "InvoiceDueAmount":
                    invoices = invoices.OrderBy(s => s.Due_Amount);
                    break;
                case "InvoiceDueAmount_desc":
                    invoices = invoices.OrderByDescending(s => s.Due_Amount);
                    break;
                case "InvoiceDueDate":
                    invoices = invoices.OrderBy(s => s.DueDate);
                    break;
                case "InvoiceDueDate_desc":
                    invoices = invoices.OrderByDescending(s => s.DueDate);
                    break;
                case "InvoicePaymentTerm":
                    invoices = invoices.OrderBy(s => s.Payment_Term);
                    break;
                case "InvoicePaymentTerm_desc":
                    invoices = invoices.OrderByDescending(s => s.Payment_Term);
                    break;

                case "InvoiceServiceType":
                    invoices = invoices.OrderBy(s => s.ServiceType.ServiceTypeName);
                    break;
                case "InvoiceServiceType_desc":
                    invoices = invoices.OrderByDescending(s => s.ServiceType.ServiceTypeName);
                    break;
                default:
                    invoices = invoices.OrderBy(s => s.Customer.Business_Name);
                    break;
            }

            //store the total amount of invoices
            ViewBag.TotalInvoiceAmount = invoices.Sum(s => s.Invoice_Amount);


            int pageSize = 10;
            int pageNumber = (page ?? 1);

            //TODO i need to create the paged feature using AJAX instead of Paged
            return View(invoices.ToPagedList(pageNumber, pageSize));
       
        }

        // GET: /Invoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            return View(InvoiceDB.GetAllInvoices.Where(s=>s.Invoice_ID==id).FirstOrDefault());
          
        }

        // GET: /Invoice/Create
        public ActionResult Create(int? CustomerID )
        {

            ViewBag.CustomersList = new SelectList(CustomerDB.Customers.OrderBy(s => s.ID_Code), "CustomerId", "ID_Code", CustomerID);
            ViewBag.ServiceTypeList = new SelectList(ServiceTypeDB.GetAllServiceTypes, "ID", "ServiceTypeName");
            

            return View();
        }

        // POST: /Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Invoice_ID,Invoice_Number,Invoice_Name,Description,Invoice_Date,Invoice_Amount,Due_Amount,DueDate,Paid_Status,Payment_Term,CustomerId,Note,ServiceTypeID")] InvoiceModel invoice)
        {
            if (ModelState.IsValid)
            {

                //check if the ID_Code exists already
                if (InvoiceDB.GetAllInvoices.Where(s => s.Invoice_Number== invoice.Invoice_Number).Any())
                {

                    // ModelState.Clear();
                    ModelState.AddModelError("ExistingInvoice_Number", "The Invoice number exists already!");
                    ViewBag.CustomersList = new SelectList(CustomerDB.Customers, "CustomerId", "ID_Code");
                    ViewBag.ServiceTypeList = new SelectList(ServiceTypeDB.GetAllServiceTypes, "ID", "ServiceTypeName");
                    return View(invoice);
                }
                
                
                
                
                
                InvoiceDB.Invoices.Add(invoice);
                InvoiceDB.Commit();
                TempData["NotificationMessage"] = "The Invoice was created successfully!";
                return RedirectToAction("Index");
            }


            ViewBag.CustomersList = new SelectList(CustomerDB.Customers, "CustomerId", "ID_Code");
            return View(invoice);
          
        }


        

        public ActionResult Search(string search)
        {
            //var result = db.Invoices
            //   .Include("Customer")
            //   .Where(x => x.Customer.Business_Name.StartsWith(search));
                           
            //return View(result.ToList());
            return View();
        }

        public ActionResult Payment(int Invoice_ID, string Business_Name)
        {
            return RedirectToAction("CreatePayment", new RouteValueDictionary(
            new { controller = "Payment", action = "CreatePayment", Invoice_ID = Invoice_ID, Business_Name = Business_Name }));
        }

        
        
        
        // GET: /Invoice/Create
        public ActionResult CreateNew(int? CustomerId, string Business_Name, string Customer_Account, int? Invoice_ID)
        {
            //if (Invoice_ID == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Invoice invoice = db.Invoices.Find(Invoice_ID);
            //ViewBag.InvoiceNumber = Customer_Account + System.DateTime.Now.Year + System.DateTime.Now.Month;


            //decimal? PreviousRemainingBalance = db.Invoices.Find(Invoice_ID).Due_Amount;
            //decimal? CPAfee = db.Invoices.Find(Invoice_ID).Invoice_Amount;

            //decimal? NewRemainingBalance = CPAfee + PreviousRemainingBalance;
            //ViewBag.RemainingBalance = NewRemainingBalance;
            
            //if (invoice == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(invoice);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNew([Bind(Include = "Invoice_ID,Invoice_Number,Invoice_Name,Description,Invoice_Date,Invoice_Amount,Due_Amount,DueDate,Paid_Status,Payment_Term,CustomerId,Note")] InvoiceModel invoice)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Invoices.Add(invoice);
            //    db.SaveChanges();
            //    return RedirectToAction("Index1", new RouteValueDictionary(
            //        new { controller = "Invoice", action = "Index1", Customer_Id = invoice.CustomerId }));
            //}

            ////ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerId", invoice.CustomerId);
            //return View(invoice);
            return View();
        }


        public ActionResult Index1(int? Customer_Id)
        {
            //return View(db.Invoices.Where(x => x.CustomerId == Customer_Id));
            return View();
        }

        public ActionResult CustomerInvoiceList(string CustomerID)
        {
            //var changeidcodetonumber = (from c in db.Customers
            //                            where c.UserId.Equals(CustomerUserName)
            //                            select c.CustomerId).FirstOrDefault();
            
            //return View(db.Invoices.Where(x => x.CustomerId.Equals(changeidcodetonumber)));
            IQueryable<InvoiceModel> InvoicesForThisCustomer = InvoiceDB.GetAllInvoices.Where(s => s.Customer.UserId == CustomerID);
            return View(InvoicesForThisCustomer.ToList());
        }


        public ActionResult InvoiceReport(string Filter)
        {
            //var result = db.Invoices
            //   .Include("Customer")
            //   .Where(x => x.Customer.Business_Name.StartsWith(Filter) && x.Invoice_Amount > 0);

            //return View(result.ToList());
            return View();
        }









        
        // GET: /Invoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceModel invoice = InvoiceDB.GetAllInvoices.Where(s=>s.Invoice_ID==id).FirstOrDefault();
            if (invoice == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "UserId", invoice.CustomerId);
            return View(invoice);
            
        }

        // POST: /Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Invoice_ID,Invoice_Number,Invoice_Name,Description,Invoice_Date,Invoice_Amount,Due_Amount,DueDate,Paid_Status,Payment_Term,CustomerId,Note")] InvoiceModel invoice)
        {
            if (ModelState.IsValid)
            {
                InvoiceDB.Update(invoice);
                TempData["NotificationMessage"] = "The account was changed successfully!";
                return RedirectToAction("Index");
            }
            //ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "UserId", invoice.CustomerId);
            return View(invoice);
          
        }

        // GET: /Invoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceModel invoice = InvoiceDB.GetAllInvoices.Where(s=>s.Invoice_ID==id).FirstOrDefault();
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
           
        }

        // POST: /Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceModel invoice = InvoiceDB.GetAllInvoices.Where(s=>s.Invoice_ID==id).FirstOrDefault();
            //TODO I need to refactor this
            InvoiceDB.Invoices.Remove(invoice);
            InvoiceDB.Commit();
            TempData["NotificationMessage"] = "The Invoice was deleted successfully!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               //TODO
                // db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult PrintInvoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            InvoiceModel invoice = InvoiceDB.Invoices.Find(id);
            AddressModel address = AddressDB.GetAllAddresses.Where(s => s.CustomerId == invoice.CustomerId).FirstOrDefault();

            string FullAddress = address.Street_Adr1 + " "
                + address.Street_Adr2 + " "
                + address.City + " "
                + address.State + " "
                + address.ZipCode;
            ViewBag.Address = FullAddress;

            var subtotal = (from o in InvoiceDB.Invoices
                            where o.Invoice_ID == id
                            select o.Invoice_Amount).Sum();
            ViewBag.Subtotal = subtotal;


            //var paymentcredit = (from p in InvoiceDB.GetAllInvoices.FirstOrDefault().Payments
            //                     where p.Invoice_ID == id
            //                     select (decimal?)p.Amount).Sum() ?? 0;

            var paymentcredit = PaymentDB.GetAllPayments.Where(s => s.Invoice_ID == id).Sum(s => (int?)s.Amount);
            if (paymentcredit == null)
                paymentcredit = 0;

            ViewBag.PaymentCredit = paymentcredit;


            ViewBag.Grandtotal = subtotal - paymentcredit;

            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
            //return View();
        }
    }
}
