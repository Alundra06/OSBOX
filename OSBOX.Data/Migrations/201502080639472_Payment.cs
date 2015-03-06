namespace OSBOX.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payment : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.PaymentModels", newName: "Payment");
            //RenameTable(name: "dbo.PaymentHistoryModels", newName: "PaymentHistory");
            //RenameTable(name: "dbo.PaymentTypeModels", newName: "Payment_Type");
            //RenameColumn(table: "dbo.Payment", name: "Payment_TypeID", newName: "Payment_Type_ID");
            //AlterColumn("dbo.Invoice", "Invoice_Number", c => c.String(nullable: false));
            //DropColumn("dbo.Payment", "Payment_TypeID");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Payment", "Payment_TypeID", c => c.Int());
            //AlterColumn("dbo.Invoice", "Invoice_Number", c => c.String());
            //RenameColumn(table: "dbo.Payment", name: "Payment_Type_ID", newName: "Payment_Type_Payment_Type_ID");
            //RenameTable(name: "dbo.Payment_Type", newName: "PaymentTypeModels");
            //RenameTable(name: "dbo.PaymentHistory", newName: "PaymentHistoryModels");
            //RenameTable(name: "dbo.Payment", newName: "PaymentModels");
        }
    }
}
