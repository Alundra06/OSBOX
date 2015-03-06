using OSBOX.Data.Models;
using System.Linq;


namespace OSBOX.Data.DAL
{
    public interface ICustomerContext
    {
        System.Data.Entity.IDbSet<OSBOX.Data.Models.CustomerModel> Customers { get; set; }
        void Commit();
        void DisposeOfObject();
        void Update(CustomerModel cm);
        void Add(CustomerModel cm);
        //For testing purposes
        IQueryable<CustomerModel> GetAllCustomers{ get; }
    }
}
