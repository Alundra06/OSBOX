using OSBOX.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace OSBOX.Data.DAL
{
    
     public class PaymentTypeContext : DbContext, OSBOX.Data.DAL.IPaymentTypeContext
    {
         public PaymentTypeContext()
            : base("DefaultConnection")
        { }
         public virtual IDbSet<PaymentTypeModel> PaymentTypes { get; set; }
        public void Commit()
        {
            base.SaveChanges();
        }
        public void DisposeOfObject()
        {
            base.Dispose();
        }
        public void Update(PaymentTypeModel ptm)
        {
            base.Entry(ptm).State = EntityState.Modified;
            Commit();
        }
        public void Add(PaymentTypeModel ptm)
        {
            PaymentTypes.Add(ptm);
        }
        //for testing purposes
        public IQueryable<PaymentTypeModel> GetAllPaymentTypes
        {
            get
            {
                return PaymentTypes;
            }
        }

        public PaymentTypeModel Find(int PaymentTypeID)
        {

            return GetAllPaymentTypes.Where(s => s.Payment_Type_ID == PaymentTypeID).FirstOrDefault();

        }
    }
}
