using System;
namespace OSBOX.Data.DAL
{
    public interface IServiceTypeContext
    {
        void Add(OSBOX.Data.Models.ServiceTypeModel stm);
        void Commit();
        void DisposeOfObject();
        OSBOX.Data.Models.ServiceTypeModel Find(string ServiceTypeID);
        System.Linq.IQueryable<OSBOX.Data.Models.ServiceTypeModel> GetAllServiceTypes { get; }
        System.Data.Entity.IDbSet<OSBOX.Data.Models.ServiceTypeModel> ServiceTypes { get; set; }
        void Update(OSBOX.Data.Models.ServiceTypeModel stm);
    }
}
