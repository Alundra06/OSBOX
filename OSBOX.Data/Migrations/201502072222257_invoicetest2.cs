namespace OSBOX.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicetest2 : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.InvoiceModels",
            //    c => new
            //        {
            //            Invoice_ID = c.Int(nullable: false, identity: true),
            //            Invoice_Number = c.String(),
            //            Invoice_Name = c.String(),
            //            Description = c.String(),
            //            Invoice_Date = c.DateTime(),
            //            Invoice_Amount = c.Decimal(precision: 18, scale: 2),
            //            Due_Amount = c.Decimal(precision: 18, scale: 2),
            //            DueDate = c.DateTime(),
            //            Paid_Status = c.String(),
            //            Payment_Term = c.String(),
            //            Note = c.String(),
            //            CustomerId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Invoice_ID)
            //    .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
            //    .Index(t => t.CustomerId);
            
            //CreateTable(
            //    "dbo.PaymentModels",
            //    c => new
            //        {
            //            Payment_ID = c.Int(nullable: false, identity: true),
            //            Invoice_ID = c.Int(nullable: false),
            //            Payment_TypeID = c.Int(),
            //            PaymentDate = c.DateTime(),
            //            Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Note = c.String(),
            //            Description = c.String(),
            //            receivedBy = c.String(),
            //            Payment_Type_Payment_Type_ID = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Payment_ID)
            //    .ForeignKey("dbo.InvoiceModels", t => t.Invoice_ID, cascadeDelete: true)
            //    .ForeignKey("dbo.PaymentTypeModels", t => t.Payment_Type_Payment_Type_ID)
            //    .Index(t => t.Invoice_ID)
            //    .Index(t => t.Payment_Type_Payment_Type_ID);
            
            //CreateTable(
            //    "dbo.PaymentHistoryModels",
            //    c => new
            //        {
            //            Payment_History_ID = c.Int(nullable: false, identity: true),
            //            Payment_ID = c.Int(nullable: false),
            //            Created_Date = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Payment_History_ID)
            //    .ForeignKey("dbo.PaymentModels", t => t.Payment_ID, cascadeDelete: true)
            //    .Index(t => t.Payment_ID);
            
            //CreateTable(
            //    "dbo.PaymentTypeModels",
            //    c => new
            //        {
            //            Payment_Type_ID = c.Int(nullable: false, identity: true),
            //            Payment_Name = c.String(),
            //        })
            //    .PrimaryKey(t => t.Payment_Type_ID);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.PaymentModels", "Payment_Type_Payment_Type_ID", "dbo.PaymentTypeModels");
            //DropForeignKey("dbo.PaymentHistoryModels", "Payment_ID", "dbo.PaymentModels");
            //DropForeignKey("dbo.PaymentModels", "Invoice_ID", "dbo.InvoiceModels");
            //DropForeignKey("dbo.InvoiceModels", "CustomerId", "dbo.Customer");
            //DropIndex("dbo.PaymentModels", new[] { "Payment_Type_Payment_Type_ID" });
            //DropIndex("dbo.PaymentHistoryModels", new[] { "Payment_ID" });
            //DropIndex("dbo.PaymentModels", new[] { "Invoice_ID" });
            //DropIndex("dbo.InvoiceModels", new[] { "CustomerId" });
            //DropTable("dbo.PaymentTypeModels");
            //DropTable("dbo.PaymentHistoryModels");
            //DropTable("dbo.PaymentModels");
            //DropTable("dbo.InvoiceModels");
        }
    }
}
