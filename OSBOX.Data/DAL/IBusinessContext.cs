using OSBOX.Data.Models;
using System.Linq;


namespace OSBOX.Data.DAL
{
    public interface IBusinessContext
    {
        System.Data.Entity.IDbSet<OSBOX.Data.Models.BusinessModel> Businesses { get; set; }
        void Commit();
        void DisposeOfObject();
        void Update(BusinessModel bm);
        void Add(BusinessModel bm);
        //For testing purposes
        IQueryable<BusinessModel> GetAllBusinesses{ get; }
    }
}
