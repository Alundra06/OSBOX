using System;
namespace OSBOX.Data.DAL
{
    public interface IPaymentHistoryContext
    {
        void Add(OSBOX.Data.Models.PaymentHistoryModel ptm);
        void Commit();
        void DisposeOfObject();
        OSBOX.Data.Models.PaymentHistoryModel Find(int PaymentHistoryID);
        System.Linq.IQueryable<OSBOX.Data.Models.PaymentHistoryModel> GetAllBusinesses { get; }
        System.Data.Entity.IDbSet<OSBOX.Data.Models.PaymentHistoryModel> PaymentHistorys { get; set; }
        void Update(OSBOX.Data.Models.PaymentHistoryModel ptm);
    }
}
