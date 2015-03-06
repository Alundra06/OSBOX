using System;
using System.Web;


namespace OSBOX.Common.Controllers
{
    public interface IFileController
    {
        System.Web.Mvc.ActionResult Delete(string id);
        System.Web.Mvc.ActionResult DeleteConfirmed(string id);
        OSBOX.Data.Models.FileModel UploadFileToServer(HttpPostedFileBase fileUpload, string Parent_Folder2, string LoggedInUserID, string TaskID);
        System.Web.Mvc.ActionResult Upload_File_Complete(HttpPostedFileBase fileUpload, string ParentFolderID, string TaskID);
        System.Web.Mvc.ActionResult UploadFileToDropBox(string FolderName, string FilePath);
        void Dispose(bool disposing);
        System.Web.Mvc.FileResult ReturnFileFromServer(String FilePath);
        System.Web.Mvc.ActionResult DownloadFileFromServer(String FilePath);
        System.Web.Mvc.ActionResult DownloadFileFromDropBox(String filePath);
        bool DeleteFileFromDataBase(string FolderID);
        bool DeleteFilesOnAFolderComplete(string FolderID);
        bool DeleteFilesOnATaskFolderComplete(string TaskID);

        bool DeleteFileFromDropBox(string Parent_Folder3);
        
       

    }
}
