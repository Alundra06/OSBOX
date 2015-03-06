using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSBOX.Common;
using OSBOX.Common.Controllers;

namespace OSBOX.Common.Tests.Controllers
{
     [TestClass()]
    class EmailControllerTest
    {


         [TestMethod()]
         public void SendEmailToAdminTest()
         {
             //arrange

             string Expected = "Mail Sent";
             


             //Act
             EmailController EC = new EmailController();
             
             //string Actual = EC.SendEmailUsingGoogleSMTP("", "","");
             

             ////Assert
             //Assert.AreEqual(Expected, Actual);
           


         }










    }
}
