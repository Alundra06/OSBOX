using OSBOX.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace OSBOX.Data.DAL
{
    
     public class PaymentContext : DbContext, OSBOX.Data.DAL.IPaymentContext
    {
         public PaymentContext()
            : base("DefaultConnection")
        { }
         public virtual IDbSet<PaymentModel> Payments { get; set; }
        public void Commit()
        {
            base.SaveChanges();
        }
        public void DisposeOfObject()
        {
            base.Dispose();
        }
        public void Update(PaymentModel pm)
        {
            base.Entry(pm).State = EntityState.Modified;
            Commit();
        }
        public void Add(PaymentModel pm)
        {
            Payments.Add(pm);
        }
        //for testing purposes
        public IQueryable<PaymentModel> GetAllPayments
        {
            get
            {
                return Payments;
            }
        }

        
    }
}
