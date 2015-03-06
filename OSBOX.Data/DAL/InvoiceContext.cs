using OSBOX.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace OSBOX.Data.DAL
{
    
     public class InvoiceContext : DbContext,IInvoiceContext
     {
         public InvoiceContext()
            : base("DefaultConnection")
        { }
         public virtual IDbSet<InvoiceModel> Invoices { get; set; }
        public void Commit()
        {
            base.SaveChanges();
        }
        public void DisposeOfObject()
        {
            base.Dispose();
        }
        public void Update(InvoiceModel im)
        {
            base.Entry(im).State = EntityState.Modified;
            Commit();
        }
        public void Add(InvoiceModel im)
        {
            Invoices.Add(im);
        }
        //for testing purposes
        public IQueryable<InvoiceModel> GetAllInvoices
        {
            get
            {
                return Invoices;
            }
        }
    }
}
