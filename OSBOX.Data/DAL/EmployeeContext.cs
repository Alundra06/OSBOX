using OSBOX.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace OSBOX.Data.DAL
{
    public class EmployeeContext:DbContext, OSBOX.Data.DAL.IEmployeeContext
    {
  
        public EmployeeContext()
            : base("DefaultConnection")
        { Database.SetInitializer<EmployeeContext>(new CreateDatabaseIfNotExists<EmployeeContext>()); }
        public virtual IDbSet<EmployeeModel> Employees { get; set; }
       
        public void Commit()
        {
            base.SaveChanges();
        }

        public void Add(EmployeeModel em)
        {
            Employees.Add(em);
        }
        public void DisposeOfObject()
        {
            base.Dispose();
            
        }

        public void Update(EmployeeModel em)
        {
            base.Entry(em).State = EntityState.Modified;
            Commit();
        }
        //for testing purposes
        public IQueryable<EmployeeModel> GetAllEmployees
        {
            get
            {
                return Employees;
            }
        }

    }
}