using OSBOX.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace OSBOX.Data.DAL
{

    public class PaymentHistoryContext : DbContext, OSBOX.Data.DAL.IPaymentHistoryContext
    {
        public PaymentHistoryContext()
            : base("DefaultConnection")
        { }
        public virtual IDbSet<PaymentHistoryModel> PaymentHistorys { get; set; }
        public void Commit()
        {
            base.SaveChanges();
        }
        public void DisposeOfObject()
        {
            base.Dispose();
        }
        public void Update(PaymentHistoryModel ptm)
        {
            base.Entry(ptm).State = EntityState.Modified;
            Commit();
        }
        public void Add(PaymentHistoryModel ptm)
        {
            PaymentHistorys.Add(ptm);
        }
        //for testing purposes
        public IQueryable<PaymentHistoryModel> GetAllBusinesses
        {
            get
            {
                return PaymentHistorys;
            }
        }

        public PaymentHistoryModel Find(int PaymentHistoryID)
        {

            return GetAllBusinesses.Where(s => s.Payment_History_ID == PaymentHistoryID).FirstOrDefault();

        }
    }
}
