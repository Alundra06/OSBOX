using OSBOX.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace OSBOX.Data.DAL
{
    
     public class BusinessContext : DbContext,IBusinessContext
    {
         public BusinessContext()
            : base("DefaultConnection")
        { }
         public virtual IDbSet<BusinessModel> Businesses { get; set; }
        public void Commit()
        {
            base.SaveChanges();
        }
        public void DisposeOfObject()
        {
            base.Dispose();
        }
        public void Update(BusinessModel bm)
        {
            base.Entry(bm).State = EntityState.Modified;
            Commit();
        }
        public void Add(BusinessModel bm)
        {
            Businesses.Add(bm);
        }
        //for testing purposes
        public IQueryable<BusinessModel> GetAllBusinesses
        {
            get
            {
                return Businesses;
            }
        }
    }
}
