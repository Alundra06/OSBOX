using System;
namespace OSBOX.Data.DAL
{
    public interface IAddressContext
    {
        void Add(OSBOX.Data.Models.AddressModel bm);
        System.Data.Entity.IDbSet<OSBOX.Data.Models.AddressModel> Addresses { get; set; }
        void Commit();
        void DisposeOfObject();
        System.Linq.IQueryable<OSBOX.Data.Models.AddressModel> GetAllAddresses { get; }
        void Update(OSBOX.Data.Models.AddressModel bm);
    }
}
