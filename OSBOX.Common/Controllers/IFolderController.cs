using OSBOX.Data.Models;
using System.Collections.Generic;
namespace OSBOX.Common.Controllers
{
    public interface IFolderController
    {
         System.Linq.IQueryable<FolderModel> GetAllFoldersInstance { get; }
        global::System.String CreateFolderOnServer(string FolderName, string ParentFolder); 
        // return the folder full path of the new created folder


        global::System.Web.Mvc.PartialViewResult Create_Folder_Complete(string Folder_Name, string Parent_Folder);
        global::System.Web.Mvc.ActionResult CreateFolderOnDropBox(string FolderName, string ParentFolder);
        global::System.Web.Mvc.JsonResult ReturnFoldersandFilesStructure(IEnumerable<FileModel> files, IEnumerable<FolderModel> folders);
        global::System.Web.Mvc.ActionResult Index();
        global::System.Web.Mvc.JsonResult GetFolderStructure(string Customers);
        System.Web.Mvc.PartialViewResult getFolderStructureByCustomerAccount(int? CustomerID);
        bool DeleteFolderFromDataBase(string FolderID);
        bool DeleteFolderFromDropBox(string Parent_Folder3);

        string GetFoldersIDFromCustomerAccount(string p);
        string GetIDForSystemFolder(string p);





       // ICustomerController CustomercontrollerEX { set; }
    }
}
