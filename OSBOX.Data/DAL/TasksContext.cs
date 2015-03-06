using OSBOX.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace OSBOX.Data.DAL
{
    public class TaskContext : DbContext,ITaskContext
    {
        IQueryable<TaskModel> TaskDB;
        public TaskContext(): base("DefaultConnection")
        {
            TaskDB = Tasks;
        }
        public virtual IDbSet<TaskModel> Tasks { get; set; }
        
        
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //Configure domain classes using Fluent API here

        //    base.OnModelCreating(modelBuilder);
        //}
       
        public System.Data.Entity.IDbSet<CustomerModel> CustomerModels { get; set; }

        public System.Data.Entity.IDbSet<EmployeeModel> EmployeeModels { get; set; }
        public void Commit()
        {
            base.SaveChanges();
        }
        public void DisposeOfObject()
        {
            base.Dispose();
        }


        public void Update(TaskModel tm)
        {
            base.Entry(tm).State = EntityState.Modified;
            Commit();
        }



        //for testing purposes
        //TaskContext TaskDB = new TaskContext();
        public IQueryable<TaskModel> GetAllTasks
        {
            get
            {
                return Tasks;
            }
        }











    }
}