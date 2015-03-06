using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Net;


namespace OSBOX.CRM.Controllers
{
	public class CRMOptionsController:Controller
	{


		private IServiceTypeContext ServiceTypeDB;
		 public CRMOptionsController (IServiceTypeContext ServiceTypeDBContext)
			{
				ServiceTypeDB = ServiceTypeDBContext;

			}
		public ActionResult Index()
		{

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateServiceType([Bind(Include = "ID,ServiceTypeName")] ServiceTypeModel ServiceType)
		{
			  if (ModelState.IsValid)
			  {
				  ServiceType.ID = Guid.NewGuid().ToString();
				  ServiceTypeDB.Add(ServiceType);
				  ServiceTypeDB.Commit();
                  return RedirectToAction("ServiceTypeIndex", "CRMOptions");

			  }
			  
			
			  //If we are here then something went wrong
			  return View(ServiceType);
		}

		public ActionResult CreateServiceType()
		  {
			  return View();
		  }
		public ActionResult ServiceTypeIndex()
		{

			
			return View(ServiceTypeDB.GetAllServiceTypes.ToList());
		}


        // GET: /Payment/Delete/5
        public ActionResult DeleteServiceType(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceTypeModel SType = ServiceTypeDB.Find(id);
            if (SType == null)
            {
                return HttpNotFound();
            }
            return View(SType);
        }

        // POST: /Payment/Delete/5
        [HttpPost, ActionName("DeleteServiceType")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteServiceTypeConfirmed(String id)
        {
            ServiceTypeModel SType = ServiceTypeDB.Find(id);
            ServiceTypeDB.ServiceTypes.Remove(SType);
            ServiceTypeDB.Commit();
            return RedirectToAction("ServiceTypeIndex", "CRMOptions");

        }

	   
	 
	}
}
