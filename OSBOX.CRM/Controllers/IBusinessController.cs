using OSBOX.Data.Models;
using System;
namespace OSBOX.CRM.Controllers
{
    public interface IBusinessController
    {
        System.Web.Mvc.ActionResult Create();
        System.Web.Mvc.ActionResult Create(OSBOX.Data.Models.BusinessModel business);
        System.Web.Mvc.ActionResult Delete(int? id);
        System.Web.Mvc.ActionResult DeleteConfirmed(int id);
        System.Web.Mvc.ActionResult Details(int? id);
        System.Web.Mvc.ActionResult Edit(OSBOX.Data.Models.BusinessModel business);
        System.Web.Mvc.ActionResult Edit(int? id);
        System.Web.Mvc.ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page,string SearchCriteria);
        System.Web.Mvc.ActionResult Search(string search);
    }
}
