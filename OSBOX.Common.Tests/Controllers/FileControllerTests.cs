using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;
using OSBOX.Common.Controllers;
namespace OSBOX.Common.Tests
{
    [TestClass()]
    public class FileControllerTests
    {
        [TestMethod()]
        public void SendEmailToAdminTest()
        {
            Assert.Fail();
        }
    }
}


namespace OSBOX.Common.Controllers.Tests
{
    [TestClass()]
    public class FileControllerTests
    {
        [TestMethod()]
        public void ReturnFileFromServerTest()
        {
           //arrange
            string filePath = @"C:\Users\osadellah\Source\Workspaces\OSBOX\OSBOX\OSBOX.WebSites\Uploads\OSBOX File System\Customers\DC2004\Pro ASP.NET MVC 5.pdf";  
            //act
            FileController controller = new FileController();
            FileResult actual = controller.ReturnFileFromServer(filePath);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(FileResult));
            Assert.AreEqual("application/octet-stream",actual.ContentType);
            Assert.AreEqual(actual.FileDownloadName, "Pro ASP.NET MVC 5.pdf");
        }
        [TestMethod()]
        public void DownloadFileFromServer_Exists_Test()
        {
            //arrange
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\OSBOX.Data.dll"; 
            //act
            FileController controller = new FileController();
            ActionResult actual = controller.DownloadFileFromServer(filePath);

            // Assert
           Assert.IsNotInstanceOfType(actual, typeof(System.Web.Mvc.EmptyResult));
          
        }

        [TestMethod()]
        public void DownlowdFileFromServer_DoesNotExists_Test()
        {
            //arrange
            string filePath = @"~\nonexisting folder"; ;
            //act
            FileController controller = new FileController();
            ActionResult actual = controller.DownloadFileFromServer(filePath);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(System.Web.Mvc.EmptyResult));

        }

        
    }
}
