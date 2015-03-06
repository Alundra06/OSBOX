using System;
namespace OSBOX.Common.Controllers
{
    interface IEmployeeController
    {
        System.Web.Mvc.ActionResult Delete(int id);
        System.Web.Mvc.ActionResult DeleteConfirmed(int id);
        System.Web.Mvc.ActionResult Details(int id);
        System.Web.Mvc.ActionResult Edit(OSBOX.Data.Models.EmployeeModel employeemodel);
        System.Web.Mvc.ActionResult Edit(int id);
        System.Web.Mvc.ActionResult Index(string sortOrder);
        System.Web.Mvc.ActionResult ResetPassword(int id);
    }
}
