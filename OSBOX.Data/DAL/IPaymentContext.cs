using System;
namespace OSBOX.Data.DAL
{
    public interface IPaymentContext
    {
        void Add(OSBOX.Data.Models.PaymentModel pm);
        void Commit();
        void DisposeOfObject();
        System.Linq.IQueryable<OSBOX.Data.Models.PaymentModel> GetAllPayments { get; }
        System.Data.Entity.IDbSet<OSBOX.Data.Models.PaymentModel> Payments { get; set; }
        void Update(OSBOX.Data.Models.PaymentModel pm);
    }
}
