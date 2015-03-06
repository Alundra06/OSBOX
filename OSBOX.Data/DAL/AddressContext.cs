
using OSBOX.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace OSBOX.Data.DAL
{

    public class AddressContext : DbContext,IAddressContext
    {
        public AddressContext()
            : base("DefaultConnection")
        { }
        public virtual IDbSet<AddressModel> Addresses { get; set; }
        public void Commit()
        {
            base.SaveChanges();
        }
        public void DisposeOfObject()
        {
            base.Dispose();
        }
        public void Update(AddressModel bm)
        {
            base.Entry(bm).State = EntityState.Modified;
            Commit();
        }
        public void Add(AddressModel bm)
        {
            Addresses.Add(bm);
        }
        //for testing purposes
        public IQueryable<AddressModel> GetAllAddresses
        {
            get
            {
                return Addresses;
            }
        }



      
    }
}
