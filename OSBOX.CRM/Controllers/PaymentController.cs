using OSBOX.Common.Infrastructure;
using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using OSBOX.Data.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace OSBOX.CRM.Controllers
{
    //Allow loggedin users only
    [AuthorizeAuthenticatedOnly]
    public class PaymentController : Controller, IPaymentController
    {

        private IPaymentContext PaymentDB;
        private ICustomerContext CustomerDB;
        private IPaymentTypeContext PaymentTypeDB;
        private IInvoiceContext InvoiceDB;

        public PaymentController(IPaymentContext PaymentDBContext,ICustomerContext CustomerDBContext, IPaymentTypeContext PaymentTypeDBContext,InvoiceContext InvoiceDBContext)
        {
            PaymentDB = PaymentDBContext;
            CustomerDB = CustomerDBContext;
            PaymentTypeDB = PaymentTypeDBContext;
            InvoiceDB = InvoiceDBContext;

        }



        // GET: /Payment/
        public ActionResult Index(int? CustomerId, System.DateTime? startDate, System.DateTime? endDate)
        {
            
            //To show a notification message
            if (TempData["NotificationMessage"] != null)
            {
                ViewBag.Notificationmessage = TempData["NotificationMessage"];
            }
            IQueryable<PaymentModel> payments;

            if(CustomerId!=null)
            {
                payments = PaymentDB.GetAllPayments.Where(s=>s.Invoice.CustomerId==CustomerId);
            }
            else
            {
                payments = PaymentDB.GetAllPayments;
            }
            if (startDate != null)
            {
                payments = payments.Where(s => s.PaymentDate >= startDate);
            }
            if (endDate != null)
            {
                payments = payments.Where(s => s.PaymentDate <= endDate);
            }
           






            ViewBag.CustomersList = new SelectList(CustomerDB.Customers, "CustomerId", "ID_Code","Select a business ID");
            ViewBag.TotalNumberofPayments = payments.Count();
            ViewBag.TotalAmountofPayments = payments.Sum(s => (int?)s.Amount);
            return View(payments.ToList());
        
        }
        public ActionResult PaymentHistory(int? Invoice_ID)
        {
            if (Invoice_ID != null)
            {

                ViewBag.TotalNumberofPayments = PaymentDB.GetAllPayments.Where(s => s.Invoice_ID == Invoice_ID).Count();
                ViewBag.TotalAmountofPayments = PaymentDB.GetAllPayments.Where(s => s.Invoice_ID == Invoice_ID).Sum(s => (int?)s.Amount);
            }
            else
            {

                return HttpNotFound();
            }


            //To show a notification message
            if (TempData["NotificationMessage"] != null)
            {
                ViewBag.Notificationmessage = TempData["NotificationMessage"];
            }
            IQueryable<PaymentModel> payments;
            payments = PaymentDB.GetAllPayments.Where(s => s.Invoice_ID == Invoice_ID);
            return View(payments.ToList());

        }
        // GET: /Payment/Details/5
        public ActionResult Details(int? id)
        {
            
            //TODO
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Payment payment = db.Payments.Find(id);
            //if (payment == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(payment);
            return View();
        }
        // GET: /Payment/Create
        //public ActionResult Create()
        //{
        //    //ViewBag.Invoice_ID = new SelectList(db.Invoices, "Invoice_ID", "Invoice_ID");
        //    //ViewBag.Payment_TypeID = new SelectList(db.Payment_Type, "Payment_Type_ID", "Payment_Name");
        //    return View();
        //}

        // GET: /CreatePayment/Create
        public ActionResult Create(int Invoice_ID)
        {

            CreatePaymentViewModel model = new CreatePaymentViewModel();
            //ViewBag.Invoice_ID = new SelectList(db.Invoices, "Invoice_ID", "Invoice_ID");

            // int tempID = db.Invoices.Where(a=>a.Invoice_ID == Invoice_ID).First().CustomerId;
            //int tempID = db.Invoices.Find(Invoice_ID).CustomerId;
            //ViewBag.Business_Name = db.Customers.Where(a => a.CustomerId == tempID).First().Business_Name;
            //ViewBag.Business_Name = CustomerDB.GetAllCustomers.Where(s => s.Invoices.FirstOrDefault().Invoice_ID == Invoice_ID).FirstOrDefault().Business_Name;
            //ViewBag.Customer_Account = CustomerDB.GetAllCustomers.Where(s => s.Invoices.FirstOrDefault().Invoice_ID == Invoice_ID).FirstOrDefault().ID_Code;
            model.Invoice_ID = Invoice_ID;
            model.Due_Amount = InvoiceDB.GetAllInvoices.Where(s => s.Invoice_ID == Invoice_ID).FirstOrDefault().Due_Amount;
            
            int tempCustomerID = InvoiceDB.GetAllInvoices.Where(s => s.Invoice_ID == Invoice_ID).FirstOrDefault().CustomerId;
            model.Business_Name = CustomerDB.GetAllCustomers.Where(s => s.CustomerId == tempCustomerID).FirstOrDefault().Business_Name;
            model.CustomerId = tempCustomerID;
            model.ID_Code = CustomerDB.GetAllCustomers.Where(s => s.CustomerId == tempCustomerID).FirstOrDefault().ID_Code;
            model.Invoice_Amount = InvoiceDB.GetAllInvoices.Where(s => s.Invoice_ID == Invoice_ID).FirstOrDefault().Invoice_Amount;
            model.Invoice_Number = InvoiceDB.GetAllInvoices.Where(s => s.Invoice_ID == Invoice_ID).FirstOrDefault().Invoice_Number;
           

            ViewBag.PaymentTypeList = new SelectList(PaymentTypeDB.GetAllPaymentTypes, "Payment_Type_ID", "Payment_Name");
            return View(model);
        }




        // POST: /Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Payment_ID,Invoice_ID,Payment_TypeID,PaymentDate,Amount,Note,Description,receivedBy,CustomerID,Payment_Type_ID")] CreatePaymentViewModel ThisPayment)
        {
            if (ModelState.IsValid)
            {
                //db.Payments.Add(payment);
                //db.SaveChanges();

                decimal? RemainAmount = InvoiceDB.Invoices.Find(ThisPayment.Invoice_ID).Due_Amount;

                //if (payment.Amount <= RemainAmount)
                //{
                InvoiceModel ThisInvoice = InvoiceDB.Invoices.Find(ThisPayment.Invoice_ID);
                //    if (myinvoice.Due_Amount == null || myinvoice.Due_Amount <= 0)
                //        myinvoice.Due_Amount = RemainAmount - payment.Amount;
                //    else
                if (ThisInvoice.Due_Amount == null)
                    ThisInvoice.Due_Amount = ThisInvoice.Invoice_Amount;
                ThisInvoice.Due_Amount -= ThisPayment.Amount;


                InvoiceDB.Update(ThisInvoice);
                //Update the Payment model
                PaymentModel pmodel = new PaymentModel();
                pmodel.Invoice_ID = ThisPayment.Invoice_ID;
                pmodel.Note = ThisPayment.Note;
                pmodel.Payment_Type_ID = ThisPayment.Payment_Type_ID;
                pmodel.Amount = ThisPayment.Amount;
                pmodel.Description = ThisPayment.Description;
                pmodel.Payment_ID = ThisPayment.Payment_ID;
                pmodel.PaymentDate = ThisPayment.PaymentDate;
                pmodel.receivedBy = ThisPayment.receivedBy;
                

                PaymentDB.Payments.Add(pmodel);
                PaymentDB.Commit();
                TempData["NotificationMessage"] = "The Payment was created successfully!";
                return RedirectToAction("Index", new { Invoice_ID = ThisPayment.Invoice_ID});
                //db.Payments.Add(payment);
                //db.SaveChanges();


        
            }

            //ViewBag.Invoice_ID = new SelectList(db.Invoices, "Invoice_ID", "Invoice_Number", payment.Invoice_ID);
            ViewBag.PaymentTypeList = new SelectList(PaymentTypeDB.GetAllPaymentTypes, "Payment_Type_ID", "Payment_Name");
            return View(ThisPayment);
            
        }

        public ActionResult Index1(int? Invoice_ID)
        {
            //return View(db.Payments.Where(x => x.Invoice_ID == Invoice_ID));
            return View();
        }


        
        
        
        
        // GET: /Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Payment payment = db.Payments.Find(id);
            //if (payment == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.Invoice_ID = new SelectList(db.Invoices, "Invoice_ID", "Invoice_Number", payment.Invoice_ID);
            //ViewBag.Payment_TypeID = new SelectList(db.Payment_Type, "Payment_Type_ID", "Payment_Name", payment.Payment_TypeID);
            //return View(payment);
            return View();
        }

        // POST: /Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Payment_ID,Invoice_ID,Payment_TypeID,PaymentDate,Amount,Note,Description,receivedBy")] PaymentModel payment)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(payment).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.Invoice_ID = new SelectList(db.Invoices, "Invoice_ID", "Invoice_Number", payment.Invoice_ID);
            //ViewBag.Payment_TypeID = new SelectList(db.Payment_Type, "Payment_Type_ID", "Payment_Name", payment.Payment_TypeID);
            //return View(payment);
            return View();
        }

        // GET: /Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Payment payment = db.Payments.Find(id);
            //if (payment == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(payment);
            return View();
        }

        // POST: /Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Payment payment = db.Payments.Find(id);
            //db.Payments.Remove(payment);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //TODO
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
