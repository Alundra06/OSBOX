using OSBOX.Common.Controllers;
using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Web.Mvc; //For JSResult







namespace OSBOX.Common.Tests
{
    [TestClass()]
    public class FolderControllerTests
    {
        // Declare global variable for Moq
        private FolderModel[] Folders = 
             
        {
            new FolderModel
             {
                 FolderID = "AID",
                 Name = "A",
                 ParentFolder = "#",
                 FullPath = "/OSBOX File System/Customers/DC3007",
                 Type="Root"
             },
             new FolderModel
             {
                FolderID = "BID",
                 Name = "B",
                 ParentFolder = "AID",
                 FullPath = "/OSBOX File System/Customers/DC2004"
                },
            new FolderModel
             {
                FolderID = "CID",
                 Name = "C",
                 ParentFolder = "AID",
                 FullPath = "/OSBOX File System/"
                },
           new FolderModel
             {
                FolderID = "DID",
                 Name = "D",
                 ParentFolder = "BID",
                 FullPath = "/OSBOX File System/Customers/",
                 Type="System"
                },

                new FolderModel
             {
                FolderID = "XID",
                 Name = "X",
                 ParentFolder = "BID",
                 FullPath = "/OSBOX File System/Customers/"
         
                },
                  new FolderModel
             {
                FolderID = "YID",
                 Name = "Y",
                 ParentFolder = "BID",
                 FullPath = "/OSBOX File System/Customers/"
 
                },
                new FolderModel
             {
                FolderID = "X1ID",
                 Name = "X1",
                 ParentFolder = "XID",
                 FullPath = "/OSBOX File System/Customers/"

                },
                new FolderModel
             {
                FolderID = "X2ID",
                 Name = "X2",
                 ParentFolder = "XID",
                 FullPath = "/OSBOX File System/Customers/"

                },
                new FolderModel
             {
                FolderID = "Y2ID",
                 Name = "Y2",
                 ParentFolder = "YID",
                 FullPath = "/OSBOX File System/Customers/"
    
                },
                new FolderModel
             {
                FolderID = "Y1ID",
                 Name = "Y1",
                 ParentFolder = "YID",
                 FullPath = "/OSBOX File System/Customers/"

                },
                      



                new FolderModel
             {
                FolderID = "EID",
                 Name = "E",
                 ParentFolder = "DID",
                 FullPath = "/OSBOX File System/Customers/"
     
                },

                 new FolderModel
             {
                FolderID = "FID",
                 Name = "F",
                 ParentFolder = "DID",
                 FullPath = "/OSBOX File System/Customers/DC2564"
                }
        };


        private FileModel[] Files = 
             
        {
            new FileModel
             {
                 FileID ="1c7fc621-928a-4bdc-bc8f-76fcb1b0303f",
                 Name = "doc.docx",
                 FolderID = "BID",
                 FilePath = @"G:\PleskVhosts\ak2cpa.com\httpdocs\Uploads\OSBOX File System\Customers\DC2004\박사논문 번역.docx",
                 FolderPath = @"/OSBOX File System/Customers/DC2004",
                 UserIdd = "645BDA35-D95A-4E54-9E8C-C03C520F09EC"

                 
                
             },
             new FileModel
             {
                 FileID ="1c7fc621-9era-4bdc-bc8f-76fcb1b0303d",
                 Name = "test.docx",
                 FolderID = "BID",
                 UploadDate = DateTime.Parse("10/1/2014 8:17:38 AM"),
                 FilePath = @"G:\PleskVhosts\ak2cpa.com\httpdocs\Uploads\OSBOX File System\Customers\DC2004\박사논문 번역.docx",
                 FolderPath = @"/OSBOX File System/Customers/DC2004",
                 UserIdd = "645BDA35-D95A-4E54-9E8C-C03C520F09EC"

                 
                
             },
              new FileModel
             {
                 FileID ="1c7fedrf-9era-4bdc-bc8f-76fcb1b0303d",
                 Name = "test.docx",
                 FolderID = "XID",
                 UploadDate = DateTime.Parse("10/1/2014 8:17:38 AM"),
                 FilePath = @"G:\PleskVhosts\ak2cpa.com\httpdocs\Uploads\OSBOX File System\Customers\DC2004\박사논문 번역.docx",
                 FolderPath = @"/OSBOX File System/Customers/DC2004",
                 UserIdd = "645BDA35-D95A-4E54-9E8C-C03C520F09EC"
             },
             new FileModel
             {
                 FileID ="xdcdc-9era-4bdc-bc8f-76fcb1b0303d",
                 Name = "testdddd.docx",
                 FolderID = "DID",
                 UploadDate = DateTime.Parse("10/1/2014 8:17:38 AM"),
                 FilePath = @"G:\PleskVhosts\ak2cpa.com\httpdocs\Uploads\OSBOX File System\Customers\DC2004\박사논문 번역.docx",
                 FolderPath = @"/OSBOX File System/Customers/DC2004",
                 UserIdd = "645BDA35-D95A-4E54-9E8C-C03C520F09EC"

                 
                
             }
             ,
             new FileModel
             {
                 FileID ="deded-9era-4bdc-bc8f-76fcb1b0303d",
                 Name = "testdddd.docx",
                 FolderID = "XID",
                 UploadDate = DateTime.Parse("10/1/2014 8:17:38 AM"),
                 FilePath = @"G:\PleskVhosts\ak2cpa.com\httpdocs\Uploads\OSBOX File System\Customers\DC2004\박사논문 번역.docx",
                 FolderPath = @"/OSBOX File System/Customers/DC2004",
                 UserIdd = "645BDA35-D95A-4E54-9E8C-C03C520F09EC"

                 
                
             }
        };



        //Declare global mock to use for all tests
        Mock<IFolderContext> mockFolderContext = new Mock<IFolderContext>();
        Mock<IFileContext> mockFileContext = new Mock<IFileContext>();




        [TestMethod()]
        public void ReturnFoldersandFilesStructureTest()
        {
            //arrange

            string expected2 = @"{ 'core': { 'data': [{ ""id"": ""0e4d204d-afb3-4bbc-8aea-0440f7f4f502"", ""parent"": ""dc3382b6-b3f9-4b75-b352-d3acac1f1f6a"", ""text"": ""DC3007"" , ""state"" : { ""opened"" : true } , ""icon"" : ""../Scripts/JSTree/dist/Folder-Generic-Green-icon.png"" ,  },{ ""id"": ""50184e78-08b1-4516-92fa-884d50351a72"", ""parent"": ""dc3382b6-b3f9-4b75-b352-d3acac1f1f6a"", ""text"": ""DC2004"" , ""state"" : { ""opened"" : true } , ""icon"" : ""../Scripts/JSTree/dist/Folder-Generic-Green-icon.png"" ,  },{ ""id"": ""1c7fc621-928a-4bdc-bc8f-76fcb1b0303f"", ""parent"": ""50184e78-08b1-4516-92fa-884d50351a72"", ""text"": ""박사논문 번역.docx"" , ""icon"" : ""../Scripts/JSTree/dist/file-icon.png"" , ""a_attr"" : { ""href"" : ""file:///G:/PleskVhosts/ak2cpa.com/httpdocs/Uploads/OSBOX File System/Customers/DC2004/박사논문 번역.docx"" }  }, ] } }";

            mockFolderContext.Setup(m => m.GetAllFolders).Returns(Folders.AsQueryable());
            mockFileContext.Setup(x => x.GetAllFiles).Returns(Files.AsQueryable());


            FolderController controller = new FolderController(mockFileContext.Object, mockFolderContext.Object, null,null);

            //Act
            JsonResult actual = controller.ReturnFoldersandFilesStructure(mockFileContext.Object.GetAllFiles, mockFolderContext.Object.GetAllFolders);



            //Assert
            Assert.AreEqual(@expected2, actual.Data);
        }

        [TestMethod()]
        public void FolderExistsTest()
        {
            //Arrange
            bool expected = true;
            string FolderID = "BID";
            mockFolderContext.Setup(m => m.GetAllFolders).Returns(Folders.AsQueryable());
            FolderController controller = new FolderController(null, mockFolderContext.Object,null,null);

            //Act
            bool actual = controller.FolderExists(FolderID);
            //Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void FolderHasSubFoldersTest()
        {
            //Arrange
            bool expected = true;
            string FolderID = "BID";
            mockFolderContext.Setup(m => m.GetAllFolders).Returns(Folders.AsQueryable());
            FolderController controller = new FolderController(null, mockFolderContext.Object, null,null);

            //Act
            bool actual = controller.FolderHasSubFolders(FolderID);
            //Assert
            Assert.AreEqual(actual, expected);



        }
        [TestMethod()]
        public void FolderHasFilesTest()
        {
            //Arrange
            bool expected = true;
            string FolderID = "BID";
            mockFileContext.Setup(x => x.GetAllFiles).Returns(Files.AsQueryable());
            FolderController controller = new FolderController(mockFileContext.Object, null, null,null);

            //Act
            bool actual = controller.FolderHasFiles(FolderID);
            //Assert
            Assert.AreEqual(actual, expected);



        }
        // TODO i need to find a better way to test this
        [Ignore]
        [TestMethod()]
        public void DeleteFolderFromDataBaseTest()
        {
            //Arrange
            bool expected = true;
            string FolderID = "50184e78-08b1-4516-92fa-884d50351a72";
            mockFolderContext.Setup(m => m.GetAllFolders).Returns(Folders.AsQueryable());
            FolderController controller = new FolderController(null, mockFolderContext.Object, null,null);

            //Act
            bool actual = controller.DeleteFolderFromDataBase(FolderID);
            //Assert
            Assert.AreEqual(actual, expected);



        }


        [TestMethod()]
        public void FolderIsSystemFolderTest()
        {
            //Arrange
            bool expected = true;
            string FolderID1 = "BID";
            string FolderID2 = "AID";
            mockFolderContext.Setup(m => m.GetAllFolders).Returns(Folders.AsQueryable());
            FolderController controller = new FolderController(null, mockFolderContext.Object, null,null);

            //Act
            bool actual1 = controller.FolderIsSystemFolder(FolderID1);
            bool actual2 = controller.FolderIsSystemFolder(FolderID2);
            //Assert
            Assert.AreEqual(actual1, expected);
            Assert.AreEqual(actual2, expected);
        }

        //[TestMethod()]
        //public void GetFoldersFromCustomerNameTest()
        //{
        //    //Arrange
        //    mockFolderContext.Setup(m => m.GetAllFolders).Returns(Folders.AsQueryable());
        //    //IQueryable<FolderModel> expected = mockFolderContext.Object.GetAllFolders.Where(s => s.Name == "DC2004" || s.Type == "Root" || s.Type == "System");
        //    FolderController controller = new FolderController(null, mockFolderContext.Object, null, null);

        //    //Act
        //    IQueryable<FolderModel> actual = controller.GetFoldersFromCustomerName("DC2004",false);
        //    var actualFolderID = actual.First().FolderID;
        //    int actualCount = actual.Count();

        //    //Assert
        //    Assert.AreEqual(actualFolderID, "50184e78-08b1-4516-92fa-884d50351a72");
        //    Assert.AreEqual(3, actualCount);
           
        //}


        [TestMethod()]
        public void GetFilesFromCustomerNameTest()
        {
            //Arrange
            mockFolderContext.Setup(m => m.GetAllFolders).Returns(Folders.AsQueryable());
            mockFileContext.Setup(m => m.GetAllFiles).Returns(Files.AsQueryable());

            FolderController controller = new FolderController(mockFileContext.Object, mockFolderContext.Object,null,null);

            //Act
            IQueryable<FileModel> actual = controller.GetFilesFromCustomerName("BID", false);
            var actualFolderID = actual.First().Name;
            int actualCount = actual.Count();

            //Assert
            Assert.AreEqual(actualFolderID, "doc.docx");
            Assert.AreEqual(5, actualCount);

        }




        [TestMethod()]
        public void GetFoldersStructutreFromFolderIDDownwardTest()
        {
            //Arrange
            mockFolderContext.Setup(m => m.GetAllFolders).Returns(Folders.AsQueryable());
            FolderController controller = new FolderController(null, mockFolderContext.Object, null,null);

            //Act
            IQueryable<FolderModel> actual = controller.GetFoldersStructutreFromFolderIDDownward("DID");

            //Assert
            Assert.AreEqual(2, actual.Count());

        }

        [TestMethod()]
        public void GetFoldersIDFromCustomerAccountTest()
        {
            //Arrange
            mockFolderContext.Setup(m => m.GetAllFolders).Returns(Folders.AsQueryable());
            FolderController controller = new FolderController(null, mockFolderContext.Object, null,null);

            //Act
            string actual = controller.GetFoldersIDFromCustomerAccount("F");

            //Assert
            Assert.AreEqual("FID", actual);

        }

        //TODO i need to move this to the FileControllerTest
        
        [TestMethod()]
        public void GetFilesFromaFolderListTest()
        {
            //Arrange
            mockFolderContext.Setup(m => m.GetAllFolders).Returns(Folders.AsQueryable());
            mockFileContext.Setup(m => m.GetAllFiles).Returns(Files.AsQueryable());
            FolderController controller = new FolderController(mockFileContext.Object, mockFolderContext.Object, null,null);

            //Act
            IQueryable<FileModel> actual = controller.GetFilesFromaFolderList(mockFolderContext.Object.GetAllFolders);


            //Assert
            Assert.AreEqual(5, actual.Count());

        }

        [TestMethod()]
        public void GetFoldersStructutreFromFolderIDUpwardTest()
        {
            //Arrange
            mockFolderContext.Setup(m => m.GetAllFolders).Returns(Folders.AsQueryable());
            FolderController controller = new FolderController(null, mockFolderContext.Object, null,null);

            //Act
            IQueryable<FolderModel> actual = controller.GetFoldersStructutreFromFolderIDUpward("X2ID");

            //Assert
            Assert.AreEqual(4, actual.Count());

        }











    }
}
