namespace AKCPA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OSBOXNewDB : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Employee",
            //    c => new
            //        {
            //            Employee_ID = c.Int(nullable: false, identity: true),
            //            FirstName = c.String(maxLength: 150),
            //            LastName = c.String(maxLength: 150),
            //            Salary_Type = c.String(maxLength: 50),
            //            Manager_ID = c.Int(),
            //            Title = c.String(maxLength: 20),
            //            Hire_Date = c.DateTime(),
            //            Salary = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
            //            Vacation_Hrs = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
            //            SickLeave_Hrs = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
            //            Created_Date = c.DateTime(),
            //            Modified_Date = c.DateTime(),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => t.Employee_ID);
            
            //CreateTable(
            //    "dbo.Task",
            //    c => new
            //        {
            //            TasksID = c.String(nullable: false, maxLength: 128),
            //            Name = c.String(),
            //            StartDate = c.DateTime(nullable: false),
            //            DueDate = c.DateTime(nullable: false),
            //            Complete = c.Int(),
            //            Description = c.String(),
            //            Priority = c.Int(),
            //            Status = c.String(),
            //            AssignementDate = c.DateTime(),
            //            CreationDate = c.DateTime(nullable: false),
            //            CompletionDate = c.DateTime(),
            //            Employee_ID = c.Int(),
            //            option = c.Boolean(),
            //            CustomerID = c.Int(),
            //        })
            //    .PrimaryKey(t => t.TasksID)
            //    .ForeignKey("dbo.Customer", t => t.CustomerID)
            //    .ForeignKey("dbo.Employee", t => t.Employee_ID)
            //    .Index(t => t.CustomerID)
            //    .Index(t => t.Employee_ID);
            
            //CreateTable(
            //    "dbo.Customer",
            //    c => new
            //        {
            //            CustomerId = c.Int(nullable: false, identity: true),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            Name_Prefix = c.String(maxLength: 10),
            //            Last_Name = c.String(maxLength: 50),
            //            Middle_name = c.String(maxLength: 50),
            //            First_Name = c.String(maxLength: 50),
            //            Business_Name = c.String(maxLength: 255),
            //            LegalName = c.String(maxLength: 255),
            //            Phone_Number = c.String(maxLength: 15),
            //            Cell_Phone = c.String(maxLength: 15),
            //            Fax = c.String(maxLength: 15),
            //            Email = c.String(maxLength: 50),
            //            ID_Code = c.String(maxLength: 255),
            //            DateStarted = c.DateTime(),
            //            DateEnded = c.DateTime(),
            //            BillingType = c.String(maxLength: 15),
            //        })
            //    .PrimaryKey(t => t.CustomerId);
            
            //CreateTable(
            //    "dbo.Address",
            //    c => new
            //        {
            //            AddressId = c.Int(nullable: false, identity: true),
            //            Street_Adr1 = c.String(),
            //            Street_Adr2 = c.String(),
            //            City = c.String(),
            //            State = c.String(),
            //            ZipCode = c.String(),
            //            Addresstype = c.String(maxLength: 1),
            //            CustomerId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.AddressId)
            //    .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
            //    .Index(t => t.CustomerId);
            
            //CreateTable(
            //    "dbo.Business",
            //    c => new
            //        {
            //            BusinessID = c.Int(nullable: false, identity: true),
            //            Description = c.String(),
            //            FIN = c.String(),
            //            CR = c.String(),
            //            SUTA = c.String(),
            //            Entity = c.String(),
            //            Filing = c.String(),
            //            BFile = c.String(),
            //            Status = c.String(),
            //            Info = c.String(),
            //            Div = c.String(),
            //            EFTPS = c.String(),
            //            CustomerId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.BusinessID)
            //    .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
            //    .Index(t => t.CustomerId);
            
            //CreateTable(
            //    "dbo.Invoice",
            //    c => new
            //        {
            //            Invoice_ID = c.Int(nullable: false, identity: true),
            //            Invoice_Number = c.String(nullable: false),
            //            Invoice_Name = c.String(),
            //            Description = c.String(),
            //            Invoice_Date = c.DateTime(nullable: false),
            //            Invoice_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Due_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            DueDate = c.DateTime(),
            //            Paid_Status = c.String(),
            //            Payment_Term = c.String(),
            //            Note = c.String(),
            //            CustomerId = c.Int(nullable: false),
            //            ServiceTypeID = c.String(maxLength: 128),
            //        })
            //    .PrimaryKey(t => t.Invoice_ID)
            //    .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
            //    .ForeignKey("dbo.ServiceType", t => t.ServiceTypeID)
            //    .Index(t => t.CustomerId)
            //    .Index(t => t.ServiceTypeID);
            
            //CreateTable(
            //    "dbo.Payment",
            //    c => new
            //        {
            //            Payment_ID = c.Int(nullable: false, identity: true),
            //            Invoice_ID = c.Int(nullable: false),
            //            Payment_Type_ID = c.Int(),
            //            PaymentDate = c.DateTime(nullable: false),
            //            Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Note = c.String(),
            //            Description = c.String(),
            //            receivedBy = c.String(),
            //        })
            //    .PrimaryKey(t => t.Payment_ID)
            //    .ForeignKey("dbo.Invoice", t => t.Invoice_ID, cascadeDelete: true)
            //    .ForeignKey("dbo.Payment_Type", t => t.Payment_Type_ID)
            //    .Index(t => t.Invoice_ID)
            //    .Index(t => t.Payment_Type_ID);
            
            //CreateTable(
            //    "dbo.PaymentHistory",
            //    c => new
            //        {
            //            Payment_History_ID = c.Int(nullable: false, identity: true),
            //            Payment_ID = c.Int(nullable: false),
            //            Created_Date = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Payment_History_ID)
            //    .ForeignKey("dbo.Payment", t => t.Payment_ID, cascadeDelete: true)
            //    .Index(t => t.Payment_ID);
            
            //CreateTable(
            //    "dbo.Payment_Type",
            //    c => new
            //        {
            //            Payment_Type_ID = c.Int(nullable: false, identity: true),
            //            Payment_Name = c.String(),
            //        })
            //    .PrimaryKey(t => t.Payment_Type_ID);
            
            //CreateTable(
            //    "dbo.ServiceType",
            //    c => new
            //        {
            //            ID = c.String(nullable: false, maxLength: 128),
            //            ServiceTypeName = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.File",
            //    c => new
            //        {
            //            FileID = c.String(nullable: false, maxLength: 128),
            //            Name = c.String(),
            //            UploadDate = c.DateTime(nullable: false),
            //            FilePath = c.String(),
            //            FolderPath = c.String(),
            //            UserIdd = c.String(maxLength: 128),
            //            FolderID = c.String(maxLength: 128),
            //            TasksID = c.String(maxLength: 128),
            //        })
            //    .PrimaryKey(t => t.FileID)
            //    .ForeignKey("dbo.Folder", t => t.FolderID)
            //    .ForeignKey("dbo.Task", t => t.TasksID)
            //    .Index(t => t.FolderID)
            //    .Index(t => t.TasksID);
            
            //CreateTable(
            //    "dbo.Folder",
            //    c => new
            //        {
            //            FolderID = c.String(nullable: false, maxLength: 128),
            //            Name = c.String(),
            //            CreationDate = c.DateTime(nullable: false),
            //            ParentFolder = c.String(),
            //            FullPath = c.String(),
            //            Type = c.String(),
            //        })
            //    .PrimaryKey(t => t.FolderID);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.File", "TasksID", "dbo.Task");
            //DropForeignKey("dbo.File", "FolderID", "dbo.Folder");
            //DropForeignKey("dbo.Task", "Employee_ID", "dbo.Employee");
            //DropForeignKey("dbo.Task", "CustomerID", "dbo.Customer");
            //DropForeignKey("dbo.Invoice", "ServiceTypeID", "dbo.ServiceType");
            //DropForeignKey("dbo.Payment", "Payment_Type_ID", "dbo.Payment_Type");
            //DropForeignKey("dbo.PaymentHistory", "Payment_ID", "dbo.Payment");
            //DropForeignKey("dbo.Payment", "Invoice_ID", "dbo.Invoice");
            //DropForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer");
            //DropForeignKey("dbo.Business", "CustomerId", "dbo.Customer");
            //DropForeignKey("dbo.Address", "CustomerId", "dbo.Customer");
            //DropIndex("dbo.File", new[] { "TasksID" });
            //DropIndex("dbo.File", new[] { "FolderID" });
            //DropIndex("dbo.Task", new[] { "Employee_ID" });
            //DropIndex("dbo.Task", new[] { "CustomerID" });
            //DropIndex("dbo.Invoice", new[] { "ServiceTypeID" });
            //DropIndex("dbo.Payment", new[] { "Payment_Type_ID" });
            //DropIndex("dbo.PaymentHistory", new[] { "Payment_ID" });
            //DropIndex("dbo.Payment", new[] { "Invoice_ID" });
            //DropIndex("dbo.Invoice", new[] { "CustomerId" });
            //DropIndex("dbo.Business", new[] { "CustomerId" });
            //DropIndex("dbo.Address", new[] { "CustomerId" });
            //DropTable("dbo.Folder");
            //DropTable("dbo.File");
            //DropTable("dbo.ServiceType");
            //DropTable("dbo.Payment_Type");
            //DropTable("dbo.PaymentHistory");
            //DropTable("dbo.Payment");
            //DropTable("dbo.Invoice");
            //DropTable("dbo.Business");
            //DropTable("dbo.Address");
            //DropTable("dbo.Customer");
            //DropTable("dbo.Task");
            //DropTable("dbo.Employee");
        }
    }
}
