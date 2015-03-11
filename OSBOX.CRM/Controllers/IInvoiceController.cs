using System;
namespace OSBOX.CRM.Controllers
{
    interface IInvoiceController
    {
        System.Web.Mvc.ActionResult Create(int? CustomerID);
        System.Web.Mvc.ActionResult Create(OSBOX.Data.Models.InvoiceModel invoice);
        System.Web.Mvc.ActionResult CreateNew(OSBOX.Data.Models.InvoiceModel invoice);
        System.Web.Mvc.ActionResult CreateNew(int? CustomerId, string Business_Name, string Customer_Account, int? Invoice_ID);
        System.Web.Mvc.ActionResult CustomerInvoiceList(string CustomerID);
        System.Web.Mvc.ActionResult Delete(int? id);
        System.Web.Mvc.ActionResult DeleteConfirmed(int id);
        System.Web.Mvc.ActionResult Details(int? id);
        System.Web.Mvc.ActionResult Edit(OSBOX.Data.Models.InvoiceModel invoice);
        System.Web.Mvc.ActionResult Edit(int? id);
        System.Web.Mvc.ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page);
        System.Web.Mvc.ActionResult Index1(int? Customer_Id);
        System.Web.Mvc.ActionResult InvoiceReport(string Filter);
        System.Web.Mvc.ActionResult Payment(int Invoice_ID, string Business_Name);
        System.Web.Mvc.ActionResult PrintInvoice(int? id);
        System.Web.Mvc.ActionResult Search(string search);
    }
}
