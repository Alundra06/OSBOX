using OSBOX.Data.Models;
using System.Linq;
namespace OSBOX.Common.Controllers
{
    public interface ICustomerController
    {
        System.Web.Mvc.ActionResult Create();
        System.Web.Mvc.ActionResult Create(OSBOX.Data.Models.CustomerModel customermodel);
        System.Web.Mvc.ActionResult Delete(int id);
        System.Web.Mvc.ActionResult DeleteConfirmed(int id);
        System.Web.Mvc.ActionResult Details(int id);
        System.Web.Mvc.ActionResult Edit(OSBOX.Data.Models.CustomerModel customermodel);
        System.Web.Mvc.ActionResult Edit(int id);
        System.Collections.Generic.IEnumerable<System.Web.Mvc.SelectListItem> GetCustomersAccounts();
        System.Web.Mvc.ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page);
        System.Web.Mvc.ActionResult ResetAllPasswords();
        System.Web.Mvc.ActionResult ResetPassword(int CustomerID);
        IQueryable<CustomerModel> GetAllCustomers();
        string GetCustomerAccountByID(int CustomerID);
        int GetCustomerIDByAccount(string CustomerAccount);
    }
}
