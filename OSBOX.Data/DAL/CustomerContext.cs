using OSBOX.Data.Models;
using System.Data.Entity;
using System.Linq;


namespace OSBOX.Data.DAL
{
    public class CustomerContext : DbContext,ICustomerContext
    {
        public CustomerContext()
            : base("DefaultConnection")
        { }
        public virtual IDbSet<CustomerModel> Customers { get; set; }
        public void Commit()
        {
            base.SaveChanges();
        }
        public void DisposeOfObject()
        {
            base.Dispose();
        }
        public void Update(CustomerModel cm)
        {
            base.Entry(cm).State = EntityState.Modified;
            Commit();
        }
        public void Add(CustomerModel cm)
        {
            Customers.Add(cm);
        }
        //for testing purposes
        public IQueryable<CustomerModel> GetAllCustomers
        {
            get
            {
                return Customers;
            }
        }
    }
}