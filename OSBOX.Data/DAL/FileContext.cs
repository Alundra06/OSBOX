using OSBOX.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace OSBOX.Data.DAL
{
    public class FileContext : DbContext, IFileContext
    {
        public FileContext()
            : base("DefaultConnection")
        {
        }
        public virtual IDbSet<FileModel> Files { get; set; }


        //public System.Data.Entity.DbSet<CustomerModel> CustomerModels { get; set; }

        public System.Data.Entity.IDbSet<TaskModel> TaskModels { get; set; }
        public System.Data.Entity.IDbSet<FolderModel> FolderModels { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //Configure domain classes using Fluent API here

        //    base.OnModelCreating(modelBuilder);
        //}
        public void Commit()
        {
            base.SaveChanges();
        }
        public void DisposeOfObject()
        {
            base.Dispose();
        }

        //for testing purposes and Moq

        public IQueryable<FileModel> GetAllFiles
        {
            get
            {
                return Files;
            }
        }

    }
}