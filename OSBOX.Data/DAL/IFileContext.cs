using OSBOX.Data.Models;
using System.Linq;
namespace OSBOX.Data.DAL
{
    public interface IFileContext
    {
        void Commit();
        void DisposeOfObject();
        System.Data.Entity.IDbSet<FileModel> Files { get; set; }
        System.Data.Entity.IDbSet<FolderModel> FolderModels { get; set; }
        System.Data.Entity.IDbSet<TaskModel> TaskModels { get; set; }

        //For testing purposes
        IQueryable<FileModel> GetAllFiles { get; }
    }
}
