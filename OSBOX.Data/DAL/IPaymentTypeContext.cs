using OSBOX.Data.Models;
using System;
namespace OSBOX.Data.DAL
{
    public interface IPaymentTypeContext
    {
        void Add(PaymentTypeModel ptm);
        void Commit();
        void DisposeOfObject();
        System.Linq.IQueryable<PaymentTypeModel> GetAllPaymentTypes { get; }
        System.Data.Entity.IDbSet<PaymentTypeModel> PaymentTypes { get; set; }
        void Update(PaymentTypeModel ptm);
        PaymentTypeModel Find(int PaymentTypeID);
    }
}
