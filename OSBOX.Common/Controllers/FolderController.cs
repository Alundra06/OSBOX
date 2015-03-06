using OSBOX.Common.Infrastructure;
using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using AppLimit.CloudComputing.SharpBox;
using Microsoft.AspNet.Identity;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSBOX.Common.Controllers
{
    //Allow loggedin users only
    [AuthorizeAuthenticatedOnly] 
    public class FolderController : Controller, IFolderController
    {


       
        
        public FolderController(IFileContext FileDBContext, IFolderContext FolderDBContext, IFileController FileControllerEx,ICustomerContext CustomerContextDB)
        {
            FileDB = FileDBContext;
            FolderDB = FolderDBContext;
            FileController = FileControllerEx;
            
            
        }

        //Property injection
        //http://ninject.codeplex.com/wikipage?title=Injection%20Patterns
        [Inject]
        public ICustomerController CustomercontrollerEX
        {
            get { return Customercontroller; }
            set { Customercontroller = value; }
     
        }
        
        private IFolderContext FolderDB;
        private ICustomerContext CustomerDB;


        private readonly static string DropboxToken = System.Web.Configuration.WebConfigurationManager.AppSettings["DropBoxToken"].ToString();

        private readonly string Token_Path = Path.Combine(HttpRuntime.AppDomainAppPath, DropboxToken);
        //Token for Viasoft
       // string Token_Path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content\\DropBox\\SharpDropBoxViaSoft.Token");

        
        
        
        
        
        
        
        
        private IQueryable<FolderModel> FolderStructure = Enumerable.Empty<FolderModel>().AsQueryable();
        
        private IFileContext FileDB;
        private IFileController FileController;
        //private ICustomerContext CustomerDB;
        private ICustomerController Customercontroller;
      

        public PartialViewResult Create_Folder_Complete(string Folder_Name, string Parent_Folder)
        {
          
          
            //Check if the logged-in user is a customer or not
            bool isCustomer = User.IsInRole("Customer");
            var folders = from s in FolderDB.Folders select s;
            var files = from t in FileDB.Files select t;


            if (!isCustomer)
            {
                //TODO I made a decision to use DropBox and not save to the server 11/08/14
                //create folder on the server
                CreateFolderOnServer(Folder_Name, Parent_Folder);

                //Create the folder on DropBox
                CreateFolderOnDropBox(Folder_Name, Parent_Folder);
            }
                
            
           // //Get the customer ID from customer-account name
           //string CustomerName = FolderDB.GetAllFolders.Where(s=>s.FolderID == Parent_Folder).First().Name;
           //string CustomerAccount = Customercontroller.GetCustomerIDByAccount(CustomerName).ToString();
            
            JsonResult js = GetFolderStructure(Parent_Folder);
                ViewBag.folders = js.Data;
                return PartialView("_Folders_Structure");
        }
        
        private string Get_Folder_Full_Path(string Parent_Folder, string Folder_Name)
        {

            //Check the Parent Folder value
            if (Parent_Folder == "")
            {
                //choose The ID of the root folder if no folder is selected
                Parent_Folder = "6f1c26c0-f2a2-40a4-ba58-1fbb1b5a75aa";
            }
            else //check if a file is seletcted
            {
               
                if (FileDB.Files.Any(a => a.FileID == Parent_Folder))
                {
                    Parent_Folder = FileDB.Files.Find(Parent_Folder).FolderID;
                }

            }
            
            
            
            
            
            
            
            String Folder_Full_Path = FolderDB.Folders.Find(Parent_Folder).FullPath + "/" + Folder_Name;
            return Folder_Full_Path;

        }
        
        //Create a new Folde on the server
        public string CreateFolderOnServer(string FolderName , string ParentFolder)
        {
            //Check the Parent Folder value
            if(ParentFolder == "")
            { 
                //choose The ID of the root folder if no folder is selected
                ParentFolder = "6f1c26c0-f2a2-40a4-ba58-1fbb1b5a75aa";
            }
            else //check if a file is seletcted
            {
                
                if (FileDB.Files.Any(a => a.FileID == ParentFolder))
                { 
                    ParentFolder = FileDB.Files.Find(ParentFolder).FolderID;
                }     
            
            }

            /////////////////////////////////////////////////////////////////////////////
            //////////////End of checking folder ID ////////////////////////////////////


            string FolderFullPath = Get_Folder_Full_Path(ParentFolder,FolderName);
           
            FolderModel MyFolder = new FolderModel()
            {
                FolderID = Guid.NewGuid().ToString(),
                Name = FolderName,
                CreationDate = DateTime.Now,
                ParentFolder = ParentFolder,
                FullPath = FolderFullPath,
                

            };
            if (ModelState.IsValid)
            {
                FolderDB.Folders.Add(MyFolder);
                
                FolderDB.Commit();
            }




           // // Create a new folder on the server
           // string ServerDirectoryPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Uploads/" + FolderFullPath);
           // //VirtualPathUtility.
           ////string ServerDirectoryPath = Server.MapPath("~/Uploads/"+Folder_Full_Path);
           // //String directoryPath = Path.Combine(Server.MapPath(Folder_Full_Path), Folder_Name);
          
           // try
           // {
           //     if (!Directory.Exists(ServerDirectoryPath))
           // {
           //     Directory.CreateDirectory(ServerDirectoryPath);
           // }
           // }
           // catch (Exception e)
           // {
           //     Console.WriteLine("The process failed: {0}", e.ToString());
           // }
            

            return FolderFullPath;
        }

        public JsonResult GetFolderStructure(string FolderID)
        {
            if (FolderID == null)
                FolderID = FolderDB.GetAllFolders.First().FolderID;
            
            //Check if the logged-in user is a customer or not
            
            bool isCustomer = User.IsInRole("Customer");
            IQueryable<FolderModel> Folders;
            IQueryable<FileModel> Files;
          
            //TODO remove after test
            
           

            if (isCustomer)
            {

                //Get the username of the logged-in user
                string CustomerName = User.Identity.GetUserName();
                FolderID = GetFoldersIDFromCustomerAccount(CustomerName);
                //Get a list of folders for the signed-in customer
                Folders = GetFolderStructureFromFolderID(FolderID, true);

            }
            else
            {
                Folders = GetFolderStructureFromFolderID(FolderID, false);
            }

            Files = GetFilesFromaFolderList(Folders);
            JsonResult FoldersandFiles = ReturnFoldersandFilesStructure(Files, Folders);
            return FoldersandFiles;
        }
        public string GetIDForSystemFolder(string p)
        {
            return FolderDB.GetAllFolders.Where(s => s.Name == p && s.Type == "System").First().FolderID;

        }
        public IQueryable<FileModel> GetFilesFromaFolderList(IQueryable<FolderModel> queryable)
        {
            
            List<FileModel> myfiles = new List<FileModel>();
            foreach (var File in FileDB.GetAllFiles)
            {
                foreach (var Folder in queryable)
                {
                    if (Folder.FolderID == File.FolderID)
                        myfiles.Add(File);
                }
                 

           
            }
            return myfiles.AsQueryable(); 
        }


        private string Type = "";
        //Recursive way to get the structure of the file system
        
        public IQueryable<FolderModel> GetFolderStructureFromFolderID(string FolderID,bool isCustomer)
        {

           
            
            GetFoldersStructutreFromFolderIDUpward(FolderID);
             GetFoldersStructutreFromFolderIDDownward(FolderID);

            //Add the system folders if it's not a customer
             //if (!isCustomer)
             //{
             //    List<FolderModel> myfolders = FolderStructure.ToList();
             //    myfolders.Add(FolderDB.GetAllFolders.Where(s => s.Type=="System").First());
             //    FolderStructure = myfolders.AsQueryable();

             //}
             return FolderStructure;
        }




        private bool FirstTime = true;
        public IQueryable<FolderModel> GetFoldersStructutreFromFolderIDDownward(string FolderID)
        {

            List<FolderModel> myfolders = FolderStructure.ToList();
            if(!FirstTime)
            myfolders.Add(FolderDB.GetAllFolders.Where(s => s.FolderID.ToLower() == FolderID.ToLower()).First());
            FirstTime = false;
            FolderStructure = myfolders.AsQueryable();
           
           
                
            foreach (var Folder in FolderDB.GetAllFolders.Where(s => s.ParentFolder == FolderID))
                {
                    GetFoldersStructutreFromFolderIDDownward(Folder.FolderID);
                }

            return FolderStructure;
        }

        public IQueryable<FolderModel> GetFoldersStructutreFromFolderIDUpward(String FolderID)
        {

            List<FolderModel> myfolders = FolderStructure.ToList();
            myfolders.Add(FolderDB.GetAllFolders.Where(s => s.FolderID.ToLower() == FolderID.ToLower()).First());
            FolderStructure = myfolders.AsQueryable();
            if(FolderDB.GetAllFolders.Where(s=>s.FolderID==FolderID).First().Type!="Root")
            {
                GetFoldersStructutreFromFolderIDUpward(FolderDB.GetAllFolders.Where(s => s.FolderID == FolderID).First().ParentFolder);  
            }
            
            
            return FolderStructure;
        }

        public IQueryable<FileModel> GetFilesFromCustomerName(string CustomerName, bool isCustomer)
        {
            String FolderID = FolderDB.GetAllFolders.Where(s => s.Name == CustomerName).FirstOrDefault().FolderID;
            if (isCustomer) // Add only the files associated with the customer
            {
                return from t in FileDB.Files where t.FolderID == FolderID select t;
            }
            else//Add the files under Tasks folder
            {
                string FolderTaskID = FolderDB.GetAllFolders.Where(s => s.Name == "Tasks").First().FolderID;
                return FileDB.GetAllFiles.Where(t=>t.FolderID == FolderID || t.FolderID == FolderTaskID) ;
            }
        }






        public JsonResult ReturnFoldersandFilesStructure(IEnumerable<FileModel> files, IEnumerable<FolderModel> folders)
        {
            //Serialize the folder item lists to return it as a JsonResult
            string SerializeFolders = "{ 'core': {'check_callback' : true, 'data': [";
                      
            
            //Get  folders
            foreach (var row in folders)
            {
                SerializeFolders += "{ \"id\": \""
                    + row.FolderID + "\", \"parent\": \""
                    + row.ParentFolder + "\", \"text\": \""
                    + row.Name + "\" , \"state\" : { \"opened\" : true }, \"data\" : { \"file\" : false } , \"";
                        
                    string Icone;
                    if (row.Type=="Root")
                        Icone = "icon\" : \"..\" ,  },";
                    else
                        Icone = "icon\" : \"../Scripts/JSTree/dist/Folder-Generic-Green-icon.png\" ,  },";
                    SerializeFolders += Icone;

            }
            
            //Get files

            foreach (var row in files)
            {
                SerializeFolders += "{ \"id\": \"" +
                    row.FileID + "\", \"parent\": \"" + row.FolderID + "\", \"text\": \"" +
                    row.Name + "\",\"data\" : { \"file\" : true }  , \"";
                //Check the extension of the file and show associated icon if available
                string Icone;
                //Get the file extenstion
                string fe = Path.GetExtension(row.Name).ToLower();
                if(fe==".pdf")
                    Icone = "icon\" : \"../Scripts/JSTree/dist/pdf-icon.png\"";
                else if (fe == ".png" || fe == ".gif" || fe == ".jpg" || fe == ".jpeg")
                    Icone = "icon\" : \"../Scripts/JSTree/dist/Image-JPEG-icon.png\"";
                else if (fe==".doc" || fe==".docx")
                    Icone = "icon\" : \"../Scripts/JSTree/dist/word-icon.png\"";
                else if (fe == ".xls" || fe == ".xlsx")
                    Icone = "icon\" : \"../Scripts/JSTree/dist/Excel-icon.png\"";
                else
                Icone  = "icon\" : \"../Scripts/JSTree/dist/file-icon.png\"";




                   SerializeFolders +=    Icone +" , \"a_attr\" : { \"href\" : \""
                    + Url.Action("../File/DownloadFileFromDropBox/", new { FilePath = row.FolderPath + "/" + row.Name }) + "\" }  },";
            }
            SerializeFolders += " ] },'contextmenu' : {'select_node' : false, 'items' : contextMenu }, 'plugins': ['contextmenu'] }";
            
            JsonResult retVal = new JsonResult();

            retVal.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            retVal.Data = SerializeFolders;
           return retVal;


        }


        public ActionResult Index()
        {

            string FirstFolderIDToDisplay;
            //TODO need to look at this
            if (TempData["FolderID"] != null)
            {
                string CurrentFolder = TempData["FolderID"].ToString();
                if (FolderDB.GetAllFolders.Where(s => s.FolderID == CurrentFolder).First().Type == "Root")
                    FirstFolderIDToDisplay = FolderDB.GetAllFolders.Where(s => s.Type == "System").First().FolderID;
                else
                FirstFolderIDToDisplay = TempData["FolderID"].ToString();
            }
            else
                //return the ID of a folder system to display only the root and the system folder
                FirstFolderIDToDisplay = FolderDB.GetAllFolders.Where(s => s.Type == "System").First().FolderID;

               
            JsonResult js = GetFolderStructure(FirstFolderIDToDisplay);
            ViewBag.folders = js.Data;
            ViewBag.CustomerNames = CustomercontrollerEX.GetCustomersAccounts();
            return View();
        }

        public PartialViewResult getFolderStructureByCustomerAccount(string CustomerID)
        {
            string FolderID;
            if (CustomerID == null)
                FolderID = FolderDB.GetAllFolders.First().FolderID;
            //If the user select "Please select a customer" option from the dropdown menu
            else if (CustomerID == "")
                FolderID = FolderDB.GetAllFolders.Where(s => s.Type == "System").First().FolderID;
            else
            {
                
                FolderID = GetFoldersIDFromCustomerAccount(CustomercontrollerEX.GetCustomerAccountByID(Convert.ToInt16(CustomerID)));
            }
            JsonResult js = GetFolderStructure(FolderID);
                ViewBag.folders = js.Data;
                return PartialView("_Folders_Structure");

        }

        public bool FolderExists(string FolderID)
        {
            return FolderDB.GetAllFolders.Any(s => s.FolderID == FolderID);
        }

        
        public string GetFoldersIDFromCustomerAccount(string p)
        {
            if (FolderDB.GetAllFolders.Where(s => s.Name == p).Any())
            {
                return FolderDB.GetAllFolders.Where(s => s.Name == p).First().FolderID;
            }
            else
           return 
               null; 
            
        }
       
        public bool FolderHasSubFolders(string FolderID)
        {
            return FolderDB.GetAllFolders.Any(s => s.ParentFolder == FolderID);
        }

        public bool FolderHasFiles(string FolderID)
        {
            return FileDB.GetAllFiles.Any(s => s.FolderID == FolderID);
        }

        public bool  DeleteFolderFromDataBase(string FolderID)
        {
            FolderDB.Folders.Remove(FolderDB.GetAllFolders.Where(s => s.FolderID == FolderID).First());
            FolderDB.Commit();
            return true;
            
        }

        public bool DeleteFolderFromDropBox(string Parent_Folder3)
       {


            
            string FolderPath = FolderDB.GetAllFolders.Where(s=>s.FolderID == Parent_Folder3).First().FullPath;
            CloudStorage dropBoxStorage = new CloudStorage();
            var dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);
            ICloudStorageAccessToken accessToken = null;
            using (FileStream fs = System.IO.File.Open(Token_Path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                accessToken = dropBoxStorage.DeserializeSecurityToken(fs);
            }

            var storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken);
            

            String UserFolderName = "/public" + FolderPath;


            try
            {
                //var fEntry = dropBoxStorage.GetFolder(UserFolderName, true);
                bool isRemoved = dropBoxStorage.DeleteFileSystemEntry(UserFolderName);
            }
            catch(Exception e)
            {

            }
            
           

            dropBoxStorage.Close();
            return true;
        }

        
        //Check if the folder exists on DropBox or not
        public bool CheckIfFolderExsistsDropBox(string Parent_Folder3)
        {


            string FolderPath = FolderDB.GetAllFolders.Where(s => s.FolderID == Parent_Folder3).First().FullPath;
            //Token for OSBOX
            //string Token_Path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content\\DropBox\\SharpDropBox.Token");
            //Token for Viasoft
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

            String UserFolderName = "/public" + FolderPath;
            bool exception = true;
            dropBoxStorage.GetFolder(UserFolderName,exception);
            
            dropBoxStorage.Close();
            return true;
        }















        public PartialViewResult DeleteFolderComplete(string Parent_Folder3)
        {
             //Store the fileID we need to send back to the view
            string ParentFolderID = FolderDB.GetAllFolders.Where(s => s.FolderID == Parent_Folder3).First().ParentFolder;
            if (!FolderExists(Parent_Folder3))
            {
                ViewBag.Message = "Couldn't find the folder!";
            }

            else //The folder exists
            {
                if (FolderIsSystemFolder(Parent_Folder3))
                {

                    ViewBag.Message = "Cannot delete a System folder";
                }

                else //The folder is not a system folder
                {
                    if (FolderHasSubFolders(Parent_Folder3))
                    {
                        ViewBag.Message = "Cannot delete a folder that has subfolders, Please contact the Administrator";
                    }

                    else
                    {
                        if(FolderHasFiles(Parent_Folder3))
                        {
                            FileController.DeleteFilesOnAFolderComplete(Parent_Folder3);
                        }
                       
                        DeleteFolderFromDropBox(Parent_Folder3);
                        DeleteFolderFromDataBase(Parent_Folder3);
                        ViewBag.Message = "Folder has been deleted";

                    }
                }
            }



            JsonResult js = GetFolderStructure(ParentFolderID);
            ViewBag.folders = js.Data;
            return PartialView("_Folders_Structure");
        }

        private void DeleteFilesFromFolder(string Parent_Folder3)
        {
            throw new NotImplementedException();
        }



        public bool FolderIsSystemFolder(string FolderID)
        {

            return FolderDB.GetAllFolders.Any(s => s.FolderID == FolderID && (s.Type == "System" || s.Type == "Root"));
        }

        
        public bool CreateFoldersForAllCustomers()
        {

            string ParentFolderID = FolderDB.GetAllFolders.Where(s => s.Type == "Root").First().FolderID;
            var Customers = CustomercontrollerEX.GetAllCustomers();
            foreach (var f in Customers)
            {
                Create_Folder_Complete(f.ID_Code, ParentFolderID);
            }

            
            return true;
        }



        /////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ///////////////////////////////Drop Box Integration/////////////////
        ////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////
        public ActionResult CreateFolderOnDropBox(string FolderName, string ParentFolder)
        {


            string FolderPath = Get_Folder_Full_Path(ParentFolder, FolderName);


            
          


            //string ServerDirectoryPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Uploads/" + Folder_Full_Path);
            CloudStorage dropBoxStorage = new CloudStorage();

            var dropBixConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox);

            ICloudStorageAccessToken accessToken = null;

            using (FileStream fs = System.IO.File.Open(Token_Path, FileMode.Open, FileAccess.Read, FileShare.None))
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



















        
    }
}
