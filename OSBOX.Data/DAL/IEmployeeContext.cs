using OSBOX.Data.Models;
using System.Linq;
namespace OSBOX.Data.DAL
{
    public interface IEmployeeContext
    {
        System.Data.Entity.IDbSet<EmployeeModel> Employees { get; set; }
        void Commit();
        void Add(EmployeeModel em);
        void Update(EmployeeModel em);
        void DisposeOfObject();
        //For testing purposes
        IQueryable<EmployeeModel> GetAllEmployees { get; }
    }
}
