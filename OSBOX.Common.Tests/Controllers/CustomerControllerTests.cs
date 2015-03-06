using OSBOX.Common.Controllers;
using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace OSBOX.Common.Tests
{
    [TestClass()]
    public class CustomerControllerTests
    {
        //Initializations
        private CustomerModel[] Customers = 
        {
             new CustomerModel{CustomerId=116,Business_Name="Fashion Cleaner",ID_Code="DC2015"},
             new CustomerModel{CustomerId=2,Business_Name="Sadellah",ID_Code="Sadellah"},
            
        };

        Mock<ICustomerContext> mockCustomerContext = new Mock<ICustomerContext>();


        //Test methods
        
        
        
        [TestMethod()]
        public void GetCustomersAccountsTest()
        {
            //arrange

            int expectedID = 116;
           

            //Mock<ITaskContext> mock = new Mock<ITaskContext>();
            mockCustomerContext.Setup(m => m.GetAllCustomers).Returns(Customers.AsQueryable());

            CustomerController controller = new CustomerController(null,null,mockCustomerContext.Object);

            //Act
            int ActualCustomerAccount = controller.GetCustomerIDByAccount("DC2015");
            


            //Assert
            Assert.AreEqual(expectedID, ActualCustomerAccount);
        }





        [TestMethod()]
        public void AutoCompleteAccountIDTest()
        {
            //arrange

            string term = "D";
            string expectedlist = "fdfd";


            //Mock<ITaskContext> mock = new Mock<ITaskContext>();
            mockCustomerContext.Setup(m => m.GetAllCustomers).Returns(Customers.AsQueryable());

            CustomerController controller = new CustomerController(null, null, mockCustomerContext.Object);

            //Act
            JsonResult ActualIDList = controller.AutoCompleteAccountID(term);



            //Assert
            Assert.AreEqual(expectedlist, ActualIDList.Data.ToString());
        }







        public void GetCustomerIDByAccountTests()
        {
            //arrange

            string expectedName = "DC2015";
            string expectedID = "116";

            //Mock<ITaskContext> mock = new Mock<ITaskContext>();
            mockCustomerContext.Setup(m => m.GetAllCustomers).Returns(Customers.AsQueryable());

            CustomerController controller = new CustomerController(null, null, mockCustomerContext.Object);

            //Act
            IEnumerable<SelectListItem> ss = controller.GetCustomersAccounts();
            string ActualName = ss.ElementAt(0).Text;
            string ActualID = ss.ElementAt(0).Value;


            //Assert
            Assert.AreEqual(expectedName, ActualName);
            Assert.AreEqual(expectedID, ActualID);


        }
    }
}
