using System;
namespace OSBOX.CRM.Controllers
{
    interface IPaymentController
    {
        System.Web.Mvc.ActionResult Create(int Invoice_ID);
        System.Web.Mvc.ActionResult Create(OSBOX.Data.ViewModels.CreatePaymentViewModel payment);
        //System.Web.Mvc.ActionResult CreatePayment(int Invoice_ID);
        System.Web.Mvc.ActionResult Delete(int? id);
        System.Web.Mvc.ActionResult DeleteConfirmed(int id);
        System.Web.Mvc.ActionResult Details(int? id);
        System.Web.Mvc.ActionResult Edit(OSBOX.Data.Models.PaymentModel payment);
        System.Web.Mvc.ActionResult Edit(int? id);
        System.Web.Mvc.ActionResult Index(int? CustomerId, System.DateTime? startDate, System.DateTime? endTime);
        System.Web.Mvc.ActionResult Index1(int? Invoice_ID);
        System.Web.Mvc.ActionResult PaymentHistory(int? Invoice_ID);
    }
}
