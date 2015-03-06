namespace AKCPA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifyservicetype3PaymentViasoftech : DbMigration
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
        }
    }
}
