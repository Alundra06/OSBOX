using OSBOX.Data.Models;
using System.Linq;


namespace OSBOX.Data.DAL
{
    public interface IInvoiceContext
    {
        System.Data.Entity.IDbSet<OSBOX.Data.Models.InvoiceModel> Invoices { get; set; }
        void Commit();
        void DisposeOfObject();
        void Update(InvoiceModel im);
        void Add(InvoiceModel im);
        //For testing purposes
        IQueryable<InvoiceModel> GetAllInvoices{ get; }
    }
}
