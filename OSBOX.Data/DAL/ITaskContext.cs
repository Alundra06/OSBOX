using OSBOX.Data.Models;
using System.Linq;
namespace OSBOX.Data.DAL
{
   public interface ITaskContext
    {
        System.Data.Entity.IDbSet<OSBOX.Data.Models.CustomerModel> CustomerModels { get; set; }
        System.Data.Entity.IDbSet<OSBOX.Data.Models.EmployeeModel> EmployeeModels { get; set; }
        System.Data.Entity.IDbSet<OSBOX.Data.Models.TaskModel> Tasks { get; set; }
       
        void Commit();
        void DisposeOfObject();

        void Update(TaskModel tm);

       //For testing purposes
        IQueryable<TaskModel> GetAllTasks { get; }
    }
}
