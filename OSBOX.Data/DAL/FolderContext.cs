using OSBOX.Data.Models;
using System.Data.Entity;
using System.Linq;


namespace OSBOX.Data.DAL
{
    public class FolderContext : DbContext, IFolderContext
    {
        public FolderContext()
            : base("DefaultConnection")
        { }
        public IDbSet<FolderModel> Folders { get; set; }
        public void Commit()
        {
            base.SaveChanges();
           
        }
        public void DisposeOfObject()
        {
            base.Dispose();
        }



        //for testing purposes and Moq

        public IQueryable<FolderModel> GetAllFolders
        {
            get
            {
                return Folders;
            }
        }
    }
}
