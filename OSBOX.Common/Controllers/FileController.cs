﻿using OSBOX.Common.Infrastructure;
using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using AppLimit.CloudComputing.SharpBox;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
//using Microsoft.AspNet.Identity.EntityFramework;




namespace OSBOX.Common.Controllers
{
    //Allow loggedin users only
    [AuthorizeAuthenticatedOnly]
    public class FileController : Controller, IFileController
    {
        
        //File DB context
        private IFileContext FileDB;
        //Folder DB context
        private IFolderContext FolderDB;
        //CurrentUserInfo
        private ICurrentUser Currentuser;


        public FileController(IFileContext FileDBContext,IFolderContext FolderDBContext, ICurrentUser currentuser)
        {
            FileDB = FileDBContext;
            FolderDB = FolderDBContext;
            Currentuser = currentuser; 
        }
        public FileController()
        { }



        public ActionResult UploadFileCompleteWithRedirect(HttpPostedFileBase fileUpload, string ParentFolderID, string TaskID)
        {

            Upload_File_Complete(fileUpload,  ParentFolderID,TaskID);
            return RedirectToAction("index", "Folder");
        }


        public ActionResult Upload_File_Complete(HttpPostedFileBase fileUpload, string ParentFolderID,string TaskID)
        {

            string ss = Currentuser.DisplayName;
            ss = Currentuser.IsAuthenticated.ToString();
            ss = Currentuser.UserID.ToString();
            
            //Check if the logged-in user is a customer or not
            
            //bool isCustomer = Roles.IsUserInRole(HttpContext.User.Identity.GetUserName(), "Customer");
            //string UserName = User.Identity.GetUserName();
            if (Currentuser.IsInRole("Customer"))
            {
                ParentFolderID = FolderDB.GetAllFolders.Where(a => a.Name == Currentuser.DisplayName).First().FolderID;
            }
            
            //string CurrentUserID = User.Identity.GetUserId();
            //TODO i need to make a decision if i want to store a copy on the server or not

            //Upload the file to the server

            FileModel ThisFile =  UploadFileToServer(fileUpload, ParentFolderID,Currentuser.UserID,TaskID);
            
            ////Upload the file to a temp location on the sever
            //string TempLocationOnServer = System.AppDomain.CurrentDomain.BaseDirectory + @"Uploads\" + fileUpload.FileName;
            string tempLocation = System.AppDomain.CurrentDomain.BaseDirectory + @"Uploads\" + fileUpload.FileName; ;
            //fileUpload.SaveAs(tempLocation);


            
            //TODO: Need to review this code later 09/19/14 OS
            
           
                ////to store the full path of the file for the file system
                string FolderFullPath = FolderDB.Folders.Find(ThisFile.FolderID).FullPath;
                //string FileFullPath1 = Server.MapPath(@"\Uploads" + @FolderFullPath);
                //string FileFullPath = Path.Combine(FileFullPath1, Path.GetFileName(fileUpload.FileName));



            //Upload a copy of the file to DropBox
                UploadFileToDropBox(ThisFile.FolderPath, tempLocation);
            //return PartialView("_Folders_Structure", GetFolderStructure());
                TempData["FolderID"] = ThisFile.FolderID;  
            //return RedirectToAction("index", "Folder");
            return new EmptyResult();
 
            

        }










        public FileModel UploadFileToServer(HttpPostedFileBase fileUpload, string Parent_Folder2, string LoggedInUserID,string TaskID)
        {
            //Check the Parent Folder value
            if (Parent_Folder2 == "" || FolderDB.GetAllFolders.Where(s=>s.FolderID==Parent_Folder2).Any()==false)
            {
                //choose The ID of the root folder if no folder is selected
                Parent_Folder2 = FolderDB.GetAllFolders.Where(s=>s.Type=="Root").First().FolderID;
            }
            else //check if a file is seletcted
            {
                
                if (FileDB.Files.Any(a => a.FileID == Parent_Folder2))
                {
                    Parent_Folder2 = FileDB.Files.Find(Parent_Folder2).FolderID;
                }

            }

            /////////////////////////////////////////////////////////////////////////////
            //////////////End of checking folder ID ////////////////////////////////////



            if (fileUpload!= null)
            {


                //taction = "Index";
                //tcontroller = "File";
               
              

                
                //to store the full path of the file for the file system
                string FolderFullPath = FolderDB.Folders.Find(Parent_Folder2).FullPath;

                //to store the relative server full path of the file
                
                //string FileFullPath1 = Server.MapPath(@"\Uploads" + @FolderFullPath);
                //string FileFullPath = Path.Combine(FileFullPath1, Path.GetFileName(fileUpload.FileName));
                

                FileModel filemodel = new FileModel()
                {

                    UserIdd = LoggedInUserID,
                    Name = Path.GetFileName(fileUpload.FileName),
                    UploadDate = DateTime.Now,
                   // FilePath = FileFullPath,
                    FolderPath = FolderFullPath,
                    FileID = Guid.NewGuid().ToString(),
                    FolderID = Parent_Folder2,
                    TasksID = TaskID

                };

                if (ModelState.IsValid)
                {
                    FileDB.Files.Add(filemodel);
                    FileDB.Commit();
                }

                
                //TODO this is the part where i made decision to save to a temp position 
                //Under the uploads files instead of the server
                //Upload the file to the hosted server location

                //fileUpload.SaveAs(FileFullPath);
                //string ss = System.AppDomain.CurrentDomain.BaseDirectory;
                //string TempLocationOnServer = Server.MapPath(@"\Uploads\" + filemodel.Name);
                string TempLocationOnServer = System.AppDomain.CurrentDomain.BaseDirectory +@"Uploads\" + filemodel.Name;
                fileUpload.SaveAs(TempLocationOnServer);

                //Upload a copy to the DropBox account
                //return RedirectToAction("UploadFile", "DropBox", new { FolderName = FolderFullPath, FilePath = filemodel.FilePath, Faction = "Index", Fcontroller = "Folder" });
                return filemodel;
            }
            
            //something went wrong : display The index again
            else
                return null;
        }



        
        [Authorize]
        // GET: /File/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileModel filemodel = FileDB.Files.Find(id);
            if (filemodel == null)
            {
                return HttpNotFound();
            }
            return View(filemodel);
        }

        // POST: /File/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            FileModel filemodel = FileDB.Files.Find(id);
            FileDB.Files.Remove(filemodel);
            //FileDB.SaveChanges();
            FileDB.Commit();
            return RedirectToAction("Index");
        }

        public  void  Dispose(bool disposing)
        {
            if (disposing)
            {
                FileDB.DisposeOfObject();
            }
            base.Dispose(disposing);
            
        }


        //See more at: http://rachelappel.com/upload-and-download-files-using-asp.net-mvc#sthash.kYwmdEZV.dpuf
        public FileResult ReturnFileFromServer(String FilePath)
         {
          return File(@FilePath, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(@FilePath));
         } 

        
        
        public ActionResult DownloadFileFromServer(String FilePath)
        {
            
            //Check if the file exists or not
            if (System.IO.File.Exists(FilePath))
            {
                return ReturnFileFromServer(FilePath);
            }
            else
            {
               //TODO change empty result with a message displayed to the user that the file does not exist
                return new EmptyResult();
            }

        }

        public bool DeleteFileFromDataBase(string FileID)
        {

            var f= FileDB.GetAllFiles.Where(u => u.FileID == FileID).First();
            FileDB.Files.Remove(f);
            FileDB.Commit();
            return true;

        }
       
        public bool DeleteFilesOnAFolderComplete(string FolderID)
        {
            var Files = FileDB.GetAllFiles.Where(u => u.FolderID == FolderID);

            foreach (var f in Files)
            {
                DeleteFileFromDropBox(f.FileID);
                DeleteFileFromDataBase(f.FileID);
            }

            //DeleteFileFromDataBase(FolderID);

            return true;
           
        }
        public bool DeleteFilesOnATaskFolderComplete(string TaskID)
        {
            var Files = FileDB.GetAllFiles.Where(u => u.TasksID == TaskID);

            foreach (var f in Files)
            {
                DeleteFileFromDropBox(f.FileID);
                DeleteFileFromDataBase(f.FileID);
            }

            //DeleteFileFromDataBase(FolderID);

            return true;

        }

        public bool DeleteFileComplete(string fileID)
        {
            DeleteFileFromDropBox(fileID);
            DeleteFileFromDataBase(fileID);
            return true;
        }


        /////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ///////////////////////////////Drop Box Integration/////////////////
        ////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////


       // string Token_Path = "~/Content/DropBox/SharpDropBox.Token";

        private readonly static string DropboxToken = System.Web.Configuration.WebConfigurationManager.AppSettings["DropBoxToken"].ToString();

        private readonly string Token_Path = Path.Combine(HttpRuntime.AppDomainAppPath, DropboxToken);
        
        
        
        
        
        
        
      
        public ActionResult UploadFileToDropBox(String FolderName, String FilePath)
        {
            
            
            CloudStorage dropBoxStorage = new CloudStorage();

            var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);

            ICloudStorageAccessToken accessToken = null;

            using (FileStream fs = System.IO.File.Open(Token_Path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
            }

            var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);

            //do stuff
            //var root = dropBoxStorage.GetRoot();

            // var publicFolder = dropBoxStorage.GetFolder("/Public");

            //foreach (var folder in publicFolder)
            //{
            //    Boolean bIsDirectory = folder is ICloudDirectoryEntry;

            //    Console.WriteLine("{0}: {1}", bIsDirectory ? "DIR" : "FIL", folder.Name);
            //}

            string remoteDirName = @"/Public" + FolderName;
            dropBoxStorage.CreateFolder(remoteDirName);



            // upload a testfile from temp directory into public folder of DropBox 
            String srcFile = FilePath;
            dropBoxStorage.UploadFile(srcFile, remoteDirName);
            dropBoxStorage.Close();
            
            
            ////Delete the temoporary file on the server
            //FileInfo myfileinf = new FileInfo(srcFile);
            //myfileinf.Delete();



            return new EmptyResult();


        }

        public ActionResult DownloadFileFromDropBox(String filePath)
        {

            CloudStorage dropBoxStorage = new CloudStorage();

            var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);

            ICloudStorageAccessToken accessToken = null;

            using (FileStream fs = System.IO.File.Open(Token_Path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
            }

            var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);

            string srcFile = @"/Public" + filePath;




            // Download the file 

            //dropBoxStorage.DownloadFile(srcFile, DestDirName);
            string TempPath = System.AppDomain.CurrentDomain.BaseDirectory + @"/Uploads";
            dropBoxStorage.DownloadFile(srcFile, TempPath);
            dropBoxStorage.Close();
            return File(Path.Combine(TempPath, Path.GetFileName(srcFile)), "application/force-download", Path.GetFileName(srcFile));

        }



        public bool DeleteFileFromDropBox(string FileID)
        {


            string FilePath = FileDB.GetAllFiles.Where(s => s.FileID == FileID).First().FolderPath + "/" +
                FileDB.GetAllFiles.Where(t=>t.FileID == FileID).First().Name;

            //string Token_Path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content\\DropBox\\SharpDropBoxViaSoft.Token");
            //string ServerDirectoryPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Uploads/" + Folder_Full_Path);
            CloudStorage dropBoxStorage = new CloudStorage();

            var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);

            ICloudStorageAccessToken accessToken = null;

            using (FileStream fs = System.IO.File.Open(Token_Path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
            }

            var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);

            String UserFolderName = "/public" + FilePath;
            try { dropBoxStorage.DeleteFileSystemEntry(UserFolderName); }
            catch (Exception e)
            {
                dropBoxStorage.Close();
                return false;
            }
           
            dropBoxStorage.Close();
            return true;
        }

    }
}
