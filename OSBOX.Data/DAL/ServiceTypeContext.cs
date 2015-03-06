using OSBOX.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace OSBOX.Data.DAL
{

    public class ServiceTypeContext : DbContext, OSBOX.Data.DAL.IServiceTypeContext
    {
        public ServiceTypeContext()
            : base("DefaultConnection")
        { }
        public virtual IDbSet<ServiceTypeModel> ServiceTypes { get; set; }
        public void Commit()
        {
            base.SaveChanges();
        }
        public void DisposeOfObject()
        {
            base.Dispose();
        }
        public void Update(ServiceTypeModel stm)
        {
            base.Entry(stm).State = EntityState.Modified;
            Commit();
        }
        public void Add(ServiceTypeModel stm)
        {
            ServiceTypes.Add(stm);
        }
        //for testing purposes
        public IQueryable<ServiceTypeModel> GetAllServiceTypes
        {
            get
            {
                return ServiceTypes;
            }
        }

        public ServiceTypeModel Find(string ServiceTypeID)
        {

            return GetAllServiceTypes.Where(s => s.ID == ServiceTypeID).FirstOrDefault();

        }

       

      
    }
}
