using OSBOX.Data.Models;
using System.Linq;
namespace OSBOX.Data.DAL
{
   public  interface IFolderContext
    {
        //System.Data.Entity.IDbSet<OSBOX.Data.Models.FileModel> FileModels { get; set; }
        System.Data.Entity.IDbSet<OSBOX.Data.Models.FolderModel> Folders { get; set; }
        void Commit();
        void DisposeOfObject();
       // void RemoveEntity(FolderModel folderModel);

        //For testing purposes
        IQueryable<FolderModel> GetAllFolders { get; }
        
    }
}
