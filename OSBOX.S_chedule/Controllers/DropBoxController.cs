﻿using OSBOX.Common.Infrastructure;
using AppLimit.CloudComputing.SharpBox;
using System;
using System.IO;
using System.Web.Mvc;

namespace OSBOX.S_chedule.Controllers
{
    //Allow loggedin users only
    [AuthorizeAuthenticatedOnly]
    public class DropBoxController : Controller
    {
        //
        // GET: /DropBox/
        
        //Token for OSBOX
        //private string TokenPath = "~/Content/DropBox/SharpDropBox.Token";
        
        //Token for Viasoft
        private string TokenPath = "~/Content/DropBox/SharpDropBoxViaSoft.Token";
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateFolder(string FolderPath)
        {
            //string Token_Path = "~/Content/DropBox/SharpDropBox.Token";
            CloudStorage dropBoxStorage = new CloudStorage();

            var dropBixConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);

            ICloudStorageAccessToken accessToken = null;

            using (FileStream fs = System.IO.File.Open(Server.MapPath(TokenPath), FileMode.Open, FileAccess.Read, FileShare.None))
            {
                accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
            }
            
            var storageToken = dropBoxStorage.Open(dropBixConfig, accessToken);

            String UserFolderName = "/public" + FolderPath;
            dropBoxStorage.CreateFolder(UserFolderName);
            dropBoxStorage.Close();

            //return RedirectToAction(taction, tcontroller);
            return new EmptyResult();

        }
        public ActionResult UploadFile(String FolderName, String FilePath, string Faction, string Fcontroller)
        {

            CloudStorage dropBoxStorage = new CloudStorage();

            var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);

            ICloudStorageAccessToken accessToken = null;

            using (FileStream fs = System.IO.File.Open(Server.MapPath(TokenPath), FileMode.Open, FileAccess.Read, FileShare.None))
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
            //Delete the temoporary file on the server
            FileInfo myfileinf = new FileInfo(srcFile);
            myfileinf.Delete();



            return RedirectToAction(Faction, Fcontroller, new { message = "File was uploaded successfully" });


        }
        public ActionResult DownloadFile(String filePath, String taction, String tcontroller)
        {

            CloudStorage dropBoxStorage = new CloudStorage();

            var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);

            ICloudStorageAccessToken accessToken = null;

            using (FileStream fs = System.IO.File.Open(Server.MapPath(TokenPath), FileMode.Open, FileAccess.Read, FileShare.None))
            {
                accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
            }

            var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);

            string srcFile = @"/Public/" + filePath;




            // Download the file 

            //dropBoxStorage.DownloadFile(srcFile, DestDirName);

            dropBoxStorage.DownloadFile(srcFile, Server.MapPath("~/Uploads"));
            dropBoxStorage.Close();
            //return RedirectToAction(taction, tcontroller, new { message = "File was downloaded successfully to your desktop" });
            return File(Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(srcFile)), "application/force-download", Path.GetFileName(srcFile));

        }
    }
}