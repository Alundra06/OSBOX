using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using OSBOX.S_chedule.Controllers;//It's included in System.Web.Extensions
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;





namespace OSBOX.S_chedule.Tests
{
    [TestClass()]
    public class TaskControllerTests
    {


        private TaskModel[] Tasks = 
        {
             new TaskModel{TasksID ="0dc6bccd-0d3e-44f9-ac59-a3ff24b10cae",
                    Name="Fashion Clnr - 2012 B/S Update",
                    StartDate=DateTime.Parse("6/16/2014 12:00:00 AM"),
                    DueDate= DateTime.Parse("7/18/2014 12:00:00 AM"),
                    Complete = 30,
                    Description = "[6/23/2014] Need rest of 2013 stmt.",
                    Priority=1,
                    Status="Waiting for Owner",
                    AssignementDate=null,
                    CreationDate=DateTime.Parse("6/16/2014 11:34:50 AM"),
                    Employee_ID=30,
                    CustomerID=116,
                    CompletionDate=null,
                    option=null
                },
                new TaskModel{TasksID ="10c1c75d-47d3-49c0-8b54-a4da1d33f06c",
                    Name="[WCT] Amend 2013 Payroll-Monique Anderson",
                    StartDate=DateTime.Parse("9/3/2014 12:00:00 AM"),
                    DueDate= DateTime.Parse("9/11/2014 12:00:00 AM"),
                    Complete = 20,
                    Description = "Convert Monique Anderson from 1099 worker to W-2 employee for 2013. Amend all the related returns.",
                    Priority=1,
                    Status="In Progress",
                    AssignementDate=DateTime.Parse("9/3/2014 12:00:00 AM"),
                    CreationDate=DateTime.Parse("9/3/2014 3:42:34 PM"),
                    Employee_ID=30,
                    CustomerID=null,
                    CompletionDate=null,
                    option=null
                },
                new TaskModel{TasksID ="3293c210-55e1-48e4-95da-e34af054efd1",
                    Name="WCT Martial Art - 2013 Tax",
                    StartDate=DateTime.Parse("7/9/2014 12:00:00 AM"),
                    DueDate= DateTime.Parse("7/16/2014 12:00:00 AM"),
                    Complete = 100,
                    Description = "Prepare 2013 tax return",
                    Priority=1,
                    Status="Completed",
                    AssignementDate=null,
                    CreationDate=DateTime.Parse("7/9/2014 8:27:01 AM"),
                    Employee_ID=30,
                    CustomerID=null,
                    CompletionDate=DateTime.Parse("8/8/2014 12:00:00 AM"),
                    option=null
                    
                }
            
        };
        private CustomerModel[] Customers = 
        {
             new CustomerModel{CustomerId=116,Business_Name="Fashion Cleaner",ID_Code="DC2015"},
             new CustomerModel{CustomerId=2,Business_Name="Sadellah",ID_Code="Sadellah"},
            
        };

        private EmployeeModel[] Employees = 
        {
             new EmployeeModel{Employee_ID = 30,FirstName="Jung",LastName="Moon"},
             new EmployeeModel{Employee_ID = 26,FirstName="Hye",LastName="Lee"},
            
        };


        Mock<ITaskContext> mockTaskContext = new Mock<ITaskContext>();
        Mock<ICustomerContext> mockCustomerContext = new Mock<ICustomerContext>();
        Mock<IEmployeeContext> mockEmployeeContext = new Mock<IEmployeeContext>();



        //[Ignore]
        [TestMethod()]
        public void IndexTest()
        {
         //arrange
            string expected = "jhdgfjg3";

            

            mockTaskContext.Setup(m => m.GetAllTasks).Returns(new TaskModel[]
            {
            
                new TaskModel{TasksID ="jhdgfjg",Name="task for testing",Status="waiting on someone"},
                new TaskModel{TasksID ="jhdgfjg2",Name="task for testing2",Status="waiting on someone2"},
                new TaskModel{TasksID ="jhdgfjg3",Name="task for testing3",Status="waiting on someone3"}
            }.AsQueryable());

            TaskController controller = new TaskController(mockTaskContext.Object, mockCustomerContext.Object,mockEmployeeContext.Object);

            //Act
            //var actual = (List<CustomerModel>)controller.Index().Model;
            string actual = controller.Index2(expected);
            //var ar = controller.Index5() as ViewResult;

            //Assert
            Assert.AreEqual(expected, actual);
            //Assert.AreEqual("Test Free", ar.ViewData["Test"]);
           
            //Assert.IsInstanceOfType(typeof(List<CustomerModel>), actual.Model);

            

        }

        [TestMethod()]
        public void GetTasksTest()
        {
            //arrange

            string expected2 = @"[{""TaskName"":""Fashion Clnr - 2012 B/S Update^/task/Details/0dc6bccd-0d3e-44f9-ac59-a3ff24b10cae^_self"",""Complete"":30,""Status"":""Waiting for Owner"",""Description"":""[6/23/2014] Need rest of 2013 stmt."",""StartDate"":""06/16/2014"",""DueDate"":""07/18/2014"",""Assignedto"":""Jung - Moon"",""Customer"":""DC2015 - Fashion Cleaner"",""Update"":""Update^/task/Edit/0dc6bccd-0d3e-44f9-ac59-a3ff24b10cae^_self"",""Delete"":""Delete^/task/Delete/0dc6bccd-0d3e-44f9-ac59-a3ff24b10cae^_self""},{""TaskName"":""[WCT] Amend 2013 Payroll-Monique Anderson^/task/Details/10c1c75d-47d3-49c0-8b54-a4da1d33f06c^_self"",""Complete"":20,""Status"":""In Progress"",""Description"":""Convert Monique Anderson from 1099 worker to W-2 employee for 2013. Amend all the related returns."",""StartDate"":""09/03/2014"",""DueDate"":""09/11/2014"",""Assignedto"":""Jung - Moon"",""Customer"":"""",""Update"":""Update^/task/Edit/10c1c75d-47d3-49c0-8b54-a4da1d33f06c^_self"",""Delete"":""Delete^/task/Delete/10c1c75d-47d3-49c0-8b54-a4da1d33f06c^_self""},{""TaskName"":""WCT Martial Art - 2013 Tax^/task/Details/3293c210-55e1-48e4-95da-e34af054efd1^_self"",""Complete"":100,""Status"":""Completed"",""Description"":""Prepare 2013 tax return"",""StartDate"":""07/09/2014"",""DueDate"":""07/16/2014"",""Assignedto"":""Jung - Moon"",""Customer"":"""",""Update"":""Update^/task/Edit/3293c210-55e1-48e4-95da-e34af054efd1^_self"",""Delete"":""Delete^/task/Delete/3293c210-55e1-48e4-95da-e34af054efd1^_self""}]";
           
            //Mock<ITaskContext> mock = new Mock<ITaskContext>();
            mockTaskContext.Setup(m => m.GetAllTasks).Returns(Tasks.AsQueryable());
            mockCustomerContext.Setup(m => m.GetAllCustomers).Returns(Customers.AsQueryable());
            mockEmployeeContext.Setup(m => m.GetAllEmployees).Returns(Employees.AsQueryable());

            TaskController controller = new TaskController(mockTaskContext.Object,mockCustomerContext.Object,mockEmployeeContext.Object);

            //Act
            JsonResult actual = controller.ReturnTasksforGrid(mockTaskContext.Object.GetAllTasks);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string Actual = serializer.Serialize(actual.Data);
            
            
            //Assert
            Assert.AreEqual(@expected2, Actual);
        }

    }
}
